using System.Security.Claims;
using AutoMapper;
using BLL.Models.Auth;
using BLL.Models.UserModels;
using BLL.Services;
using Common.Consts;
using Common.Exceptions.General;
using Common.Generics;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ezdomawka.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly EmailService _emailService;
        private readonly AdminService _adminService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public AuthController(UserService userService, IMapper mapper, AuthService authService, EmailService emailService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _mapper = mapper;
            _authService = authService;
            _emailService = emailService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (returnUrl != null) ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest request, string? returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userService.GetUserByCredentials(_mapper.Map<CredentialModel>(request));
                    await Authenticate(user);
                    if (returnUrl != null) return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                return View(request);
            }
            catch (NotFoundException)
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return View(request);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterExtensionRequest request)
        {
            if (await _userService.CheckUserExistByEmail(request.Email)) ModelState.AddModelError(nameof(DAL.Entities.User.Email), "Пользователь с такой почтой уже существует");
            if (await _userService.CheckUserExistByNick(request.Nick)) ModelState.AddModelError(nameof(DAL.Entities.User.Nick), "Пользователь с таким ником уже существует");
            if (!_emailService.CheckCorrectConfirmCode(request.Email, request.ConfirmCode.ToString())) ModelState.AddModelError(nameof(RegisterExtensionRequest.ConfirmCode), "Неправильный код");
            if (ModelState.IsValid)
            {
                User user = await _authService.RegisterUser(_mapper.Map<RegisterModel>(request));
                await Authenticate(user); 
                return Ok();
            }
            return BadRequest();
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nick),
                new Claim(Claims.UserClaim, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToString()),
                new Claim(Claims.EmailClaim, user.Email)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            await _userService.DeleteUser(User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim));
            return await Logout();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(RegisterRequest request)
        {
            if(await _userService.CheckUserExistByEmail(request.Email)) ModelState.AddModelError(nameof(DAL.Entities.User.Email), "Пользователь с такой почтой уже существует");
            if(await _userService.CheckUserExistByNick(request.Nick)) ModelState.AddModelError(nameof(DAL.Entities.User.Nick), "Пользователь с таким ником уже существует");
            if (ModelState.IsValid)
            {
                await _emailService.SendConfirmCodeToEmailAsync(request.Email, _webHostEnvironment.WebRootPath);
                return Ok();
            }
            return BadRequest();
        }
    }
}

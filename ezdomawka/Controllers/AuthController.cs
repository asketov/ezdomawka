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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ezdomawka.Controllers
{
    public class AuthController : BaseController, EmailService.IMailConfirmRegistrationUriGenerator
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly EmailService _emailService;
        private readonly AdminService _adminService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public AuthController(UserService userService, AuthService authService, EmailService emailService, AdminService adminService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _emailService = emailService;
            _adminService = adminService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
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
                await GenerateLoginModelStateErrors();
                return View(request);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendRegisterCode(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var model = _mapper.Map<RegisterModel>(request);

            if (await RegisterModelIsValid(model) == false)
            {
                await GenerateRegisterModelStateErrors(model);
                return View("Register", request);
            }

            await _emailService.SendRegisterFinishCodeToEmailAsync(model, _webHostEnvironment.WebRootPath, this);
            
            return MultiElementInformation(
                "Сообщение с кодом для регестрации было отправлено вам на почту", 
                $"<div class=\"head\">{model.Email}",
                "Если код не пришел проверьте точно ли указана ваша почта.");
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmRegister(Guid registerCode)
        {
            var model = await _emailService.TryGetRegisterFinishModelAsync(registerCode);

            if (model == null)
                return BadRequest();

            User user = await _authService.RegisterUser(model);
            await Authenticate(user);
            
            return SingeElementInformation("Аккаунт был создан.");
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

        public Uri GenerateUri(Guid confirmCode)
        {
            return new Uri(Url.Action("ConfirmRegister", "Auth", new { registerCode = confirmCode }, Request.Scheme));
        }
        
        private async Task<bool> RegisterModelIsValid(RegisterModel model)
        {
            return await _userService.CheckUserExistByEmail(model.Email) == false &&
                await _userService.CheckUserExistByNick(model.Nick) == false;
        }
        
        private async Task GenerateRegisterModelStateErrors(RegisterModel model)
        {
            if (await _userService.CheckUserExistByEmail(model.Email))
                ModelState.AddModelError(nameof(DAL.Entities.User.Email), "Пользователь с такой почтой уже существует");
            if (await _userService.CheckUserExistByNick(model.Nick))
                ModelState.AddModelError(nameof(DAL.Entities.User.Nick), "Пользователь с таким ником уже существует");
        }
        
        private async Task GenerateLoginModelStateErrors()
        {
            ModelState.AddModelError(nameof(DAL.Entities.User.Email), "Некорректные логин и(или) пароль");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Auth;
using BLL.Models.UserModels;
using BLL.Services;
using Common.Consts;
using Common.Exceptions.General;
using Common.Exceptions.User;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(UserService userService, IMapper mapper, AuthService authService)
        {
            _userService = userService;
            _mapper = mapper;
            _authService = authService;
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
                User user = await _userService.GetUserByCredentials(_mapper.Map<CredentialModel>(request));
                await Authenticate(user.Nick, user.Id);
                if (returnUrl != null) return Redirect(returnUrl);
                return RedirectToAction("Index", "Home");
            }
            catch(NotFoundException)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _authService.RegisterUser(_mapper.Map<RegisterModel>(request));
                    await Authenticate(user.Nick, user.Id); 
                    return RedirectToAction("Index", "Home");
                }
                return View(request);
            }
            catch (Exception ex)
            {
                if(ex is NickAlreadyExistException) ModelState.AddModelError(nameof(DAL.Entities.User.Nick), "Пользователь с таким ником уже существует");
                if(ex is EmailAlreadyExistException) ModelState.AddModelError(nameof(DAL.Entities.User.Email), "Пользователь с такой почтой уже существует");
                return View(request);
            }
        }

        private async Task Authenticate(string userName, Guid userId)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(Claims.UserClaim, userId.ToString())
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Auth;
using BLL.Models.ViewModels;
using BLL.Services;
using Common.Consts;
using Common.Exceptions.User;
using Common.Generics;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    public class UserController : Controller
    {
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _webHostBuilder;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        public UserController(EmailService emailService, IWebHostEnvironment webHostBuilder, IMapper mapper, UserService userService)
        {
            _emailService = emailService;
            _webHostBuilder = webHostBuilder;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("SendChangePasswordLinkToEmail");
            return View("InputEmail");
        }



        [HttpGet]
        public async Task<IActionResult> SendChangePasswordLinkToEmail(string email)
        {
            try
            {
                if (User.Identity!.IsAuthenticated)
                    email = User.Claims.GetClaimValueOrDefault<string>(Claims.EmailClaim)!;
                await _userService.GetUserByEmail(email);
                var myUrl = Url.ActionLink("ChangePasswordByLink", "User");
                await _emailService.SendChangePasswordLinkToEmail(email, myUrl!, _webHostBuilder.WebRootPath);
                return View("../Home/Information", "Сообщение с информацией было отправлено на вашу почту");
            }
            catch (UserNotFoundException)
            {
                ModelState.AddModelError("", "Пользователя с такой почтой нет");
                return View("InputEmail");
            }
            catch
            {
                return View("../Home/Information", "Что-то пошло не так, попробуйте позже");
            }
        }

        [HttpGet]
        public IActionResult ChangePasswordByLink(Guid code)
        {
            if (_emailService.CheckCorrectLink(code.ToString(), out string? email))
            {
                ChangePasswordVm vm = new ChangePasswordVm()
                {
                    Code = code
                };
                return View("ChangePassword", vm);
            }
            return View("../Home/Information", "Ссылка является недействительной, попробуйте еще раз");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVm request)
        {
            if (ModelState.IsValid && _emailService.CheckCorrectLink(request.Code.ToString(), out string? email))
            {
                var model = _mapper.Map<ChangePasswordModel>(request);
                model.Email = email!;
                await _userService.ChangePassword(model);
                return View("../Home/Information", "Пароль успешно изменён");
            }
            return View("../Home/Information", "Ссылка истекла, попробуйте еще раз");
        }
    }
}

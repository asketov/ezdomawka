using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Auth;
using BLL.Models.FavorSolution;
using BLL.Models.ViewModels;
using BLL.Services;
using Common.Consts;
using Common.Exceptions.User;
using Common.Generics;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ezdomawka.Controllers
{
    public class UserController : Controller
    {
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _webHostBuilder;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly FavorSolutionService _favorSolutionService;
        private readonly AdminService _adminService;
        public UserController(EmailService emailService, IWebHostEnvironment webHostBuilder, IMapper mapper, 
            UserService userService, FavorSolutionService favorSolutionService, AdminService adminService)
        {
            _emailService = emailService;
            _webHostBuilder = webHostBuilder;
            _mapper = mapper;
            _userService = userService;
            _favorSolutionService = favorSolutionService;
            _adminService = adminService;
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FavorSolutions(CancellationToken token)
        {
            var userId = User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
            var favorSolutions = await _userService.GetFavorSolutionsByUserId(userId, token);
            var vms = favorSolutions.Select(x => _mapper.Map<FavorSolutionVm>(x)).ToList();
            return View("MyFavors", vms);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditFavor(Guid id, CancellationToken token)
        {
            var solutionModel = await _favorSolutionService.GetSolutionModelById(id);
            var themes = (await _adminService.GetThemeModels()).Select(x => _mapper.Map<ThemeVm>(x)).ToList();
            var editVm = _mapper.Map<EditSolutionVm>(solutionModel);
            editVm.Themes = themes.Where(x => x.Id != solutionModel.Theme.Id).Prepend(_mapper.Map<ThemeVm>(solutionModel.Theme));
            editVm.Subjects = (await _adminService.GetSubjectModels()).Select(x => _mapper.Map<SubjectVm>(x)).OrderBy(x => x.Name);
            return View(editVm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditFavor(EditSolutionRequest request)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
                if (!await _userService.CheckUserHasFavor(userId, request.Id)) return BadRequest();
                var model = _mapper.Map<SolutionModel>(request);
                model.Author = new User() { Id = userId };
                await _favorSolutionService.UpdateFavor(model);
                return StatusCode(StatusCodes.Status200OK, new { redirect = "/home/index" });
            }
            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> DeleteFavor(Guid favorId, string? returnUrl = null)
        {
            var userId = User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
            if (await _userService.CheckUserHasFavor(userId, favorId) || User.IsInRole(Roles.SuperAdminId))
            {
                await _favorSolutionService.DeleteFavor(favorId);
                if(returnUrl == null) return RedirectToAction(nameof(FavorSolutions));
                return Redirect(returnUrl);
            }
            return BadRequest();
        }
    }
}

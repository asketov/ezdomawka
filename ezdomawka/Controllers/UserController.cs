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
    public class UserController : BaseController
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
        [Authorize]
        public IActionResult MainMenu()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("SendChangePasswordLinkToUserMail");
            return View("InputEmail");
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendChangePasswordLinkToUserMail()
        {
            if (User.Identity!.IsAuthenticated == false)
                return SomeSingWrongMessage();

            try
            {
                var email = User.Claims.GetClaimValueOrDefault<string>(Claims.EmailClaim)!;

                await SendChangePasswordLinkToMail(email);
            }
            catch
            {
                return SomeSingWrongMessage();
            }
            
            return ChangePasswordInformation();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SendChangePasswordLinkToCustomMail(string email)
        {
            try
            {
                await SendChangePasswordLinkToMail(email);
            }
            catch (UserNotFoundException)
            {
                ModelState.AddModelError("", "Пользователя с такой почтой нет");
                return View("InputEmail");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                
                return SomeSingWrongMessage();
            }

            return ChangePasswordInformation(email);
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
            return SingeElementInformation("Ссылка является недействительной, попробуйте еще раз");
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
                return SingeElementInformation("Пароль успешно изменён");
            }
            return SingeElementInformation( "Ссылка истекла, попробуйте еще раз");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> FavorSolutions()
        {
            var userId = User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
            var countTodayUpdates = await _favorSolutionService.GetTodayUpdatesFavors(userId);
            var remainUpdates = FavorConsts.UpdatesInDayLimit - countTodayUpdates;
            var favorSolutions = await _userService.GetFavorSolutionsByUserId(userId);
            var vms = favorSolutions.Select(x => _mapper.Map<FavorSolutionVm>(x)).ToList();
            var vm = new MyFavorsVm()
            {
                favors = vms, RemainUpdates = remainUpdates
            };
            return View("Favors", vm);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditFavor(Guid id, CancellationToken token)
        {
            var solutionModel = await _favorSolutionService.GetSolutionModelById(id);

            if (solutionModel == null)
                return BadRequest();
            
            var themes = (await _adminService.GetThemeModels()).Select(x => _mapper.Map<ThemeVm>(x)).ToList();
            
            var editVm = _mapper.Map<EditSolutionVm>(solutionModel);
            editVm.Themes = themes.Where(x => x.Id != solutionModel.Theme.Id).Prepend(_mapper.Map<ThemeVm>(solutionModel.Theme));
            editVm.Subjects = (await _adminService.GetSubjectModels()).Select(x => _mapper.Map<SubjectVm>(x)).OrderBy(x => x.Name);
            
            return View(editVm);
        }

        [HttpPost]
        [Authorize]
        [RequestFormLimits(ValueCountLimit = int.MaxValue)]
        public async Task<IActionResult> EditFavor(EditSolutionRequest request, string? returnLink)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
                if (!await _userService.CheckUserHasFavor(userId, request.Id)) return BadRequest();
                var countUpdates = await _favorSolutionService.GetTodayUpdatesFavors(userId);
                if (countUpdates >= FavorConsts.UpdatesInDayLimit) return StatusCode(StatusCodes.Status406NotAcceptable);
                var model = _mapper.Map<SolutionModel>(request);
                model.AuthorId = userId;
                var isUpdated = await _favorSolutionService.UpdateFavor(model);
                if(isUpdated) await _favorSolutionService.AddRecordToUpdateHistory(userId);
                return StatusCode(StatusCodes.Status200OK, new { redirect = GetRedirectLink(returnLink) });
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

        [HttpGet]
        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> IntroduceBan()
        {
            var userId = User.Claims.GetClaimValueOrDefault<Guid>(Claims.UserClaim);
            if (await _userService.UserIsBanned(userId))
            {
                var ban = await _userService.GetCurrentBanOrDefault(userId);
                if (ban != null)
                {
                    return View(_mapper.Map<BanVm>(ban));
                }
                await _userService.UnbanUser(userId);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task SendChangePasswordLinkToMail(string email)
        {
            await _userService.GetUserByEmail(email);
            var myUrl = Url.ActionLink("ChangePasswordByLink", "User");
            await _emailService.SendChangePasswordLinkToEmail(email, myUrl!, _webHostBuilder.WebRootPath);
        }
        
        private IActionResult SomeSingWrongMessage()
        {
            return SingeElementInformation( "Что-то пошло не так, попробуйте позже");
        }
        
        private IActionResult ChangePasswordInformation(string? email = null)
        {
            if (email == null)
                return SingeElementInformation("Сообщение с информацией было отправлено на вашу почту");

            return MultiElementInformation("Сообщение с информацией было отправлено на вашу почту",
            email,$"Если код не пришел проверте точно ли указана ваша почта.");
        }
    }
}

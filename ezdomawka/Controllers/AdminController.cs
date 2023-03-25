using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using BLL.Models.FavorSolution;
using BLL.Models.ViewModels;
using BLL.Services;
using Common.Consts;
using Common.Exceptions.Admin;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ezdomawka.Controllers
{
    [Authorize(Roles =  $"{Roles.SuperAdminId},{Roles.AdminId}")]
    public class AdminController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;
        private readonly UserService _userService;
        private readonly FavorSolutionService _favorSolutionService;

        public AdminController(IMapper mapper, AdminService adminService, UserService userService, FavorSolutionService favorSolutionService)
        {
            _mapper = mapper;
            _adminService = adminService;
            _userService = userService;
            _favorSolutionService = favorSolutionService;
        }
        
        [HttpGet]
        public async Task<IActionResult> ThemeManager()
        {
            var themes = await _adminService.GetThemeModels();
            return View(themes.Select(x=> _mapper.Map<ThemeVm>(x)));
        }

        [HttpGet]
        public IActionResult AddTheme()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTheme(ThemeRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _adminService.AddTheme(_mapper.Map<ThemeModel>(request));
                    return RedirectToAction(nameof(ThemeManager));
                }
                catch (ThemeAlreadyExistException)
                {
                    return View(request);
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTheme(Guid id)
        {
            try
            {
                await _adminService.DeleteTheme(id);
                return RedirectToAction(nameof(ThemeManager));
            }
            catch
            {
                return RedirectToAction("ThemeManager");
            }
        }


        
        [HttpGet]
        public async Task<IActionResult> SubjectManager()
        {
            var subjects = await _adminService.GetSubjectModels();
            return View(subjects.Select(x => _mapper.Map<SubjectVm>(x)));
        }

        [HttpGet]
        public IActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _adminService.AddOrUpdateSubject(_mapper.Map<SubjectModel>(request));
                    return RedirectToAction(nameof(SubjectManager));
                }
                catch (SubjectAlreadyExistException)
                {
                    return View(request);
                }
            }
            return BadRequest();
        }
        
        [HttpGet]
        public async Task<IActionResult> EditSubject(Guid id)
        {
            var model = await _adminService.GetSubjectModel(id);
            if(model == null) return RedirectToAction(nameof(SubjectManager));
            return View(_mapper.Map<SubjectVm>(model));
        }

        [HttpPost]
        public async Task<IActionResult> EditSubject(SubjectVm vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _adminService.AddOrUpdateSubject(_mapper.Map<SubjectModel>(vm));
                    return RedirectToAction(nameof(SubjectManager));
                }
                catch (SubjectAlreadyExistException)
                {
                    return View(vm);
                }
            }
            return BadRequest();
        }


        
        [HttpGet]
        public async Task<IActionResult> GetUserTable(UserPanelRequest request, CancellationToken token)
        {
            var userVms = await _adminService.GetUsersByRequest(request, token);
            return PartialView("Partials/_UserTable", userVms);
        }
        [HttpGet]
        public async Task<IActionResult> UserPanel(UserPanelRequest request, CancellationToken token)
        {
            var userVms = await _adminService.GetUsersByRequest(request, token);
            var count = await _adminService.GetCountUsersByFilters(request, token);
            return View(new UserPanelVm() {CountUsersByFilters = count, UserVms = userVms});
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersWithPagination(UserPanelRequest request, CancellationToken token)
        {
            var userVms = await _adminService.GetUsersByRequest(request, token);
            var count = await _adminService.GetCountUsersByFilters(request, token);
            return PartialView("Partials/_UserTableWithPagination", new UserPanelVm() { CountUsersByFilters = count, UserVms = userVms });
        }
        
        [HttpPost]
        public async Task<IActionResult> BanUser(BanRequest request)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminService.CheckUserExistById(request.UserId)) return BadRequest();
                if (await _adminService.UserIsAdmin(request.UserId)) return BadRequest();
                
                if ( await _userService.UserIsBanned(request.UserId)) return BadRequest();

                await _adminService.BanUser(request);
                return StatusCode(StatusCodes.Status200OK, new { redirect = "/home/index" });
            }
            return BadRequest();
        }
        
        [HttpPost]
        public async Task<IActionResult> UnBanUser(Guid userId)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminService.CheckUserExistById(userId)) return BadRequest();
                if (await _adminService.UserIsAdmin(userId)) return BadRequest();
                
                if (!await _userService.UserIsNotBanned(userId)) return BadRequest();

                await _adminService.UnBanUser(userId);
                return StatusCode(StatusCodes.Status200OK, new { redirect = "/home/index" });
            }
            return BadRequest();
        }



        [HttpPost]
        public async Task<ActionResult> CleanReport(CleanReportRequest request)
        {
            if (!await _adminService.CheckUserExistById(request.UserId)) throw new NullReferenceException();
            if(!await _favorSolutionService.CheckFavorExist(request.FavorId)) throw new NullReferenceException();
            if(!await _favorSolutionService.CheckFavorReportExist(request.FavorReportId)) throw new NullReferenceException();

           await _favorSolutionService.CleanReports(request.UserId, request.FavorId, request.FavorReportId);

           return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult> CleanReports(CleanAllReportsRequest request)
        {
            if (!await _adminService.CheckUserExistById(request.UserId)) throw new NullReferenceException();
            if(!await _favorSolutionService.CheckFavorExist(request.FavorId)) throw new NullReferenceException();
            await _favorSolutionService.CleanReports(request.UserId, request.FavorId);

            return Ok();
        }


        [HttpGet]
        public async Task<IEnumerable<BanVm>> GetUserBans(GetUserBansRequest request)
        {
            if (!await _adminService.CheckUserExistById(request.UserId)) throw new NullReferenceException();
            
            return await _userService.GetBans(request.UserId, skip: request.Skip, take: request.Take);
        }

        
        
        [HttpGet]
        public async Task<IEnumerable<ReportVm>> GetFavorReports(GetUserFavorReportsRequest request)
        {
            if (!await _adminService.CheckUserExistById(request.UserId)) throw new NullReferenceException();
            if(!await _favorSolutionService.CheckFavorExist(request.FavorId)) throw new NullReferenceException();
            
            return await _favorSolutionService.GetReports(request.UserId,request.FavorId,  skip: request.Skip, take: request.Take);
        }

        [HttpGet]
        public async Task<int> GetFavorReportsCount(GetUserFavorReportsCountRequest request)
        {
            if (!await _adminService.CheckUserExistById(request.UserId)) throw new NullReferenceException();
            if(!await _favorSolutionService.CheckFavorExist(request.FavorId)) throw new NullReferenceException();
            
            return await _favorSolutionService.GetReportsCount(request.UserId,request.FavorId);
        }
        
        [HttpGet]
        public async Task<IEnumerable<FavorSolutionVm>> GetUserFavorSolutions(Guid userId, CancellationToken token)
        {
            if (!await _adminService.CheckUserExistById(userId)) throw new NullReferenceException();

            var favorSolutions = await _userService.GetFavorSolutionsByUserId(userId, token);
            
            return favorSolutions.Select(x => _mapper.Map<FavorSolutionVm>(x)).ToList();
        }
    }
}

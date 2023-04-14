﻿using System;
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

        #region Theme
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
        public IActionResult EditTheme()
        {
            throw new NotImplementedException();
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
        #endregion

        #region Subject
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
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            try
            {
                await _adminService.DeleteSubject(id);
                return RedirectToAction(nameof(SubjectManager));
            }
            catch
            {
                return RedirectToAction("SubjectManager");
            }
        }
        #endregion

        #region Views
        [HttpGet]
        public async Task<IActionResult> FavorSolutionsWarnTopPage()
        {
            var solutionsTop = await GetTopSolutionsByReports(new GetTopSolutionsByReportsRequest(){Take = 10, Skip = 0});
            
            return  PartialView("_FavorSolutiosWarnTop", solutionsTop);
        }
        
        [HttpGet]
        ////Pag onclick
        public async Task<IActionResult> UserTable(UserPanelRequest request, CancellationToken token)
        {
            var userVms = await _adminService.GetUsersByRequest(request, token);
            return PartialView("Partials/_UserTable", userVms);
        }

        [HttpGet]
        ////View no change
        public async Task<IActionResult> UserPanel(UserPanelRequest request, CancellationToken token)
        {
            var userVms = await _adminService.GetUsersByRequest(request, token);
            var count = await _adminService.GetCountUsersByFilters(request, token);
            return View(new UserPanelVm() {CountUsersByFilters = count, UserVms = userVms});
        }

        ////Pag View
        [HttpGet]
        public async Task<IActionResult> UsersWithPagination(UserPanelRequest request, CancellationToken token)
        {
            var userVms = await _adminService.GetUsersByRequest(request, token);
            var count = await _adminService.GetCountUsersByFilters(request, token);
            return PartialView("Partials/_UserTableWithPagination", new UserPanelVm() { CountUsersByFilters = count, UserVms = userVms });
        }

        [HttpGet]
        public async Task<IActionResult> FavorSolutions(Guid userId)
        {
            if (!await _adminService.CheckUserExistById(userId))
                return BadRequest();
            
            var favorSolutions = await _userService.GetFavorSolutionsByUserId(userId);
            var vms = favorSolutions.Select(x => _mapper.Map<FavorSolutionVm>(x)).ToList();
            
            return View("UserFavors", vms);
        }
        
        [HttpGet]
        public async Task<IActionResult> FavorSolutionsWarnTop(Guid userId)
        {
            if (!await _adminService.CheckUserExistById(userId))
                return BadRequest();
            
            var favorSolutions = await _userService.GetFavorSolutionsByUserId(userId);
            var vms = favorSolutions.Select(x => _mapper.Map<FavorSolutionVm>(x)).ToList();
            
            return View("UserFavors", vms);
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteFavor(Guid userId, Guid favorId)
        {
            if (await _userService.CheckUserHasFavor(userId, favorId) || User.IsInRole(Roles.SuperAdminId))
            { 
                Console.WriteLine(":hfcxzvchjgdvhjkcvchyjkvchkvchkbvbhk");
                
                await _favorSolutionService.DeleteFavor(favorId);
                
                return await FavorSolutions(userId);
            }
            return BadRequest();
        }
        
        [HttpGet]
        public async Task<IActionResult> FavorReportsPage(Guid favorId)
        {
            try
            {
                var favorReports = await GetFavorReports(new GetUserFavorReportsRequest(){FavorId = favorId});

                return View(favorReports);
            }
            catch(NullReferenceException e)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> SuggestionsPage()
        {
            var suggestions = await GetSuggestions(new GetSuggestionsRequest());

            return View("Suggestions" ,suggestions);
        }
        
        public async Task<IActionResult> BanUser(Guid userId)
        {
            return View(new BanRequest(){UserId = userId});
        }
        #endregion

        #region Ban
        [HttpGet]
        public async Task<IEnumerable<BanVm>> GetUserBans(GetUserBansRequest request)
        {
            if (!await _adminService.CheckUserExistById(request.UserId)) throw new NullReferenceException();
            
            return await _userService.GetBans(request.UserId, skip: request.Skip, take: request.Take);
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
        
        [HttpGet]
        public async Task<IActionResult> UnBanUser(Guid userId)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminService.CheckUserExistById(userId)) return BadRequest();
                if (await _adminService.UserIsAdmin(userId)) return BadRequest();
                
                //if (!await _userService.UserIsNotBanned(userId)) return BadRequest();

                await _adminService.UnBanUser(userId);
                return StatusCode(StatusCodes.Status200OK, new { redirect = "/home/index" });
            }
            return BadRequest();
        }
        #endregion

        #region Report

        
        [HttpGet]
        public async Task<IEnumerable<WarnTopFavorSolutionVm>> GetTopSolutionsByReports( GetTopSolutionsByReportsRequest request)
        {
            return await _adminService.GetTopSolutionsByReports(request);
        }

        [HttpGet]
        public async Task<IEnumerable<ReportVm>> GetFavorReports(GetUserFavorReportsRequest request)
        {
            if(!await _favorSolutionService.CheckFavorExist(request.FavorId)) throw new NullReferenceException();
            
            return await _favorSolutionService.GetReports(request.FavorId,  skip: request.Skip, take: request.Take);
        }
        
        [HttpGet]
        public async Task<int> GetFavorReportsCount(Guid favorId)
        {
            if(!await _favorSolutionService.CheckFavorExist(favorId)) throw new NullReferenceException();
            
            return await _favorSolutionService.GetReportsCount(favorId);
        }
        
        [HttpGet]
        public async Task<IEnumerable<FavorSolutionVm>> GetUserFavorSolutions(Guid userId)
        {
            if (!await _adminService.CheckUserExistById(userId)) throw new NullReferenceException();

            var favorSolutions = await _userService.GetFavorSolutionsByUserId(userId);
            
            return favorSolutions.Select(x => _mapper.Map<FavorSolutionVm>(x)).ToList();
        }
        
        
        
        [HttpPost]
        public async Task<ActionResult> CleanReport(CleanReportRequest request)
        {
            if (!await _favorSolutionService.CheckFavorExist(request.FavorId)) throw new NullReferenceException();
            if(!await _favorSolutionService.CheckFavorReportExist(request.FavorReportId)) throw new NullReferenceException();

           await _favorSolutionService.CleanReports(request.FavorId, request.FavorReportId);

           return Ok();
        }
        
        [HttpPost]
        public async Task<ActionResult> CleanReports(Guid favorId)
        {
            await _favorSolutionService.CleanReports(favorId);

            return Ok();
        }
        #endregion

        #region Suggection
        [HttpGet]
        public async Task<IEnumerable<SuggestionVm>> GetSuggestions(GetSuggestionsRequest request)
        {
            return await _adminService.GetSuggestions(request);
        }
        #endregion
    }
}

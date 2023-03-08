using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
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
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;
        public AdminController(IMapper mapper, AdminService adminService)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ThemeManager()
        {
            var themes = await _adminService.GetThemeModels();
            return View(themes.Select(x=> _mapper.Map<ThemeVm>(x)));
        }
        [HttpGet]
        public async Task<IActionResult> SubjectManager()
        {
            var subjects = await _adminService.GetSubjectModels();
            return View(subjects.Select(x => _mapper.Map<SubjectVm>(x)));
        }

        [HttpGet]
        public IActionResult AddTheme()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddSubject()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditSubject(Guid id)
        {
            var model = await _adminService.GetSubjectModel(id);
            if(model == null) return RedirectToAction(nameof(SubjectManager));
            return View(_mapper.Map<SubjectVm>(model));
        }

        [HttpPost]
        public async Task<IActionResult> BanUser(BanRequest request)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminService.UserNotAdmin(request.UserId)) return BadRequest();
                await _adminService.BanUser(request);
                return Ok();

            }
            return BadRequest();
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
    }

}

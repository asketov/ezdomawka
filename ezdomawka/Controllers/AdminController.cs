using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using BLL.Models.ViewModels;
using BLL.Services;
using Common.Exceptions.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    [Authorize]
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
        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectRequest request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _adminService.AddSubject(_mapper.Map<SubjectModel>(request));
                    return RedirectToAction(nameof(SubjectManager));
                }
                catch (SubjectAlreadyExistException)
                {
                    return View(request);
                }
            }

            return BadRequest();
        }

    }

}

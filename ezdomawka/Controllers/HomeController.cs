using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using BLL.Models.ViewModels;
using BLL.Services;

namespace ezdomawka.Controllers
{
    public class HomeController : Controller
    {
        private readonly FavorSolutionService _favorSolutionService;
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;
        public HomeController(FavorSolutionService favorSolutionService, IMapper mapper, AdminService adminService)
        {
            _favorSolutionService = favorSolutionService;
            _mapper = mapper;
            _adminService = adminService;
        }
        public async Task<IActionResult> Index()
        {
            var favorSolutions = (await _favorSolutionService.GetAllSolutionModels()).Select(x => _mapper.Map<FavorSolutionVm>(x));
            var themes = (await _adminService.GetThemeModels()).Select(x => _mapper.Map<ThemeVm>(x));
            var subjects = (await _adminService.GetSubjectModels()).Select(x => _mapper.Map<SubjectVm>(x));
            var countFavor = await _favorSolutionService.GetCountSolutions();
            IndexVm vm = new IndexVm()
            {
                FavorSolutionVms = favorSolutions,
                ThemeVms = themes,
                SubjectVms = subjects,
                CountFavorSolutions = countFavor
            };
            return View(vm);
        }
    }
}
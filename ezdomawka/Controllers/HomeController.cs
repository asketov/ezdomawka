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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(FavorSolutionService favorSolutionService, IMapper mapper, AdminService adminService, IWebHostEnvironment webHostEnvironment)
        {
            _favorSolutionService = favorSolutionService;
            _mapper = mapper;
            _adminService = adminService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var favorSolutions = (await _favorSolutionService.GetSolutionModels(skip : 0, take : 10, token)).Select(x => _mapper.Map<FavorSolutionVm>(x));
            var themes = (await _adminService.GetThemeModels()).Select(x => _mapper.Map<ThemeVm>(x));
            var subjects = (await _adminService.GetSubjectModels()).Select(x => _mapper.Map<SubjectVm>(x));
            var countFavor = await _favorSolutionService.GetCountSolutions();
            IndexVm vm = new IndexVm()
            {
                ThemeVms = themes,
                SubjectVms = subjects,
                FavorsWithPaginationVm = new FavorsWithPaginationVm()
                {
                    FavorSolutionVms = favorSolutions, CountFavorSolutions = countFavor
                }
            };
            return View(vm);
        }
    }
}
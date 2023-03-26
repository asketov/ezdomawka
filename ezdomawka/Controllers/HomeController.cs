using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AutoMapper;
using BLL.Models.ViewModels;
using BLL.Services;
using DAL.Entities;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Xml;
using System;
using System.ServiceModel.Syndication;
using AutoMapper.Internal;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace ezdomawka.Controllers
{
    public class HomeController : BaseController
    {
        private readonly FavorSolutionService _favorSolutionService;
        private readonly AdminService _adminService;
        private readonly HomeService _homeService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(FavorSolutionService favorSolutionService, 
            IMapper mapper, 
            AdminService adminService, 
            IWebHostEnvironment webHostEnvironment,
            HomeService homeService)
        {
            _favorSolutionService = favorSolutionService;
            _mapper = mapper;
            _adminService = adminService;
            _webHostEnvironment = webHostEnvironment;
            _homeService = homeService;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var favorSolutions = (await _favorSolutionService.GetSolutionModels(skip : 0, take : 10, token)).Select(x => _mapper.Map<FavorSolutionVm>(x));
            var themes = (await _adminService.GetThemeModels()).Select(x => _mapper.Map<ThemeVm>(x));
            var subjects = (await _adminService.GetSubjectModels()).Select(x => _mapper.Map<SubjectVm>(x)).OrderBy(x => x.Name);
            var countFavor = await _favorSolutionService.GetCountSolutions();
            IndexVm vm = new IndexVm()
            {
                ThemeVms = themes,
                SubjectVms = subjects,
                FavorsWithPaginationVm = new FavorsWithPaginationVm()
                {
                    FavorSolutionVms = favorSolutions, CountFavorSolutions = countFavor, FavorSolutionsOffset = 0
                }
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Rools()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSuggestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSuggestion(SuggestionVm request)
        {
            try
            {
                var model = _mapper.Map<Suggestion>(request);
                await _homeService.AddSuggestion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return SingeElementInformation( "Что-то пошло не так");
            }
        }
    }
}
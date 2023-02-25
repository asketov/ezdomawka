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
    public class HomeController : Controller
    {
        private readonly FavorSolutionService _favorSolutionService;
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;
        private readonly HomeService _homeService;
        private readonly DataContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(FavorSolutionService favorSolutionService, 
            IMapper mapper, 
            AdminService adminService, 
            IWebHostEnvironment webHostEnvironment,
            HomeService homeService, DataContext db)
        {
            _favorSolutionService = favorSolutionService;
            _mapper = mapper;
            _adminService = adminService;
            _webHostEnvironment = webHostEnvironment;
            _homeService = homeService;
            _db = db;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var favorSolutions = (await _favorSolutionService.GetSolutionModels(skip : 0, take : 10, token)).Select(x => _mapper.Map<FavorSolutionVm>(x));
            var themes = (await _adminService.GetThemeModels()).Select(x => _mapper.Map<ThemeVm>(x));
            var subjects = (await _adminService.GetSubjectModels()).Select(x => _mapper.Map<SubjectVm>(x)).ToList();
            subjects.Sort();
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

        [HttpGet]
        public ActionResult Rools()
        {
            //HashSet<string> predmetu = new HashSet<string>();
            //List<XmlNode> list = new List<XmlNode>();
            //XmlDocument xDoc = new XmlDocument();
            //xDoc.Load("https://www.voenmeh.ru/templates/jd_atlanta/js/TimetableGroup31.xml");
            //// получим корневой элемент
            //XmlElement? xRoot = xDoc.DocumentElement;
            //if (xRoot != null)
            //{
            //    // обход всех узлов в корневом элементе
            //    foreach (XmlElement xnode in xRoot.ChildNodes)
            //    {
            //        // получаем атрибут name
            //        if (xnode.Name != "Group") continue;
            //        if (xnode.LastChild == null) continue;
            //        foreach (var el1 in xnode.ChildNodes!)
            //        {
            //            foreach (var el2 in ((XmlNode)el1).ChildNodes)
            //            {
            //                foreach (var el3 in ((XmlNode)el2).ChildNodes)
            //                {
            //                    foreach (var el4 in ((XmlNode)el3).ChildNodes)
            //                    {
            //                        foreach (var el in ((XmlNode)el4).ChildNodes)
            //                        {
            //                            list.Add((XmlNode)el);
            //                        }
            //                        var m = list[3].InnerText.IndexOf(' ');
            //                        var l = list[3].InnerText.Substring(m + 1);
            //                        predmetu.Add(l);
            //                        list.Clear();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            // var sh = predmetu.ToList();
            // sh.Sort();
            // foreach (var k in sh)
            // {
            //     _db.Subjects.Add(new Subject() {Name = k});
            // }
            ////_db.Subjects.AddRange(sh.Select(x => new Subject() { Name = x }));
            //_db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult AddSuggestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddSuggestion(SuggestionVm request)
        {
            try
            {
                var model = _mapper.Map<Suggestion>(request);
                await _homeService.AddSuggestion(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("../Home/Information", "Что-то пошло не так");
            }
        }
    }
}
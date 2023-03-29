using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    public class ModalController : BaseController
    {
        private readonly FavorSolutionService _favorSolutionService;

        public ModalController(FavorSolutionService favorSolutionService)
        {
            _favorSolutionService = favorSolutionService;
        }

        [HttpGet]
        public IActionResult GetReportModal()
        {
            return View("ReportModal");
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjectsModal(Guid favorId, int skip, int take)
        {
            var subjects = await _favorSolutionService.GetFavorSubjects(favorId, skip, take);
            return View("SubjectsModal", subjects);
        }
        [HttpGet]
        public IActionResult GetBanModal()
        {
            return View("BanModal");
        }
    }
}

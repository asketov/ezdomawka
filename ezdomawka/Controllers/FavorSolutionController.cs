using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.FavorSolution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    [Authorize]
    public class FavorSolutionController : Controller
    {
        public FavorSolutionController()
        {

        }
        [HttpGet]
        public IActionResult AddSolution()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSolution(AddSolutionRequest request)
        {
            return View(request);
        }
    }
}

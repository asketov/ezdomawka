using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    public class FavorSolutionController : Controller
    {
        public FavorSolutionController()
        {

        }
        public IActionResult AddSolution()
        {
            return View();
        }
    }
}

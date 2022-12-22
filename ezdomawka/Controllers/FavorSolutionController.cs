using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.FavorSolution;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    [Authorize]
    public class FavorSolutionController : Controller
    {
        private readonly FavorSolutionService _favorSolutionService;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public FavorSolutionController(FavorSolutionService favorSolutionService, IMapper mapper, UserService userService)
        {
            _favorSolutionService = favorSolutionService;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult AddSolution()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSolution(AddSolutionRequest request)
        {
            try
            {
                var model = _mapper.Map<AddSolutionModel>(request);
                model.Author = await _userService
                    .GetUserById(Guid.Parse(User.Claims.FirstOrDefault(u => u.Type == "userId")!.Value));
                await _favorSolutionService.AddFavor(model);
                return View(request);
            }
            catch
            {
                return RedirectToAction(nameof(AddSolution));
            }
        }
    }
}

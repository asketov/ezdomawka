using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.FavorSolution;
using BLL.Services;
using Common.Consts;
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
        public async Task<IActionResult> AddSolution()
        {
            var model = await _favorSolutionService.GetAddSolutionModel();
            var f = _mapper.Map<AddSolutionRequest>(model);
            return View(f);
        }

        [HttpPost]
        public async Task<IActionResult> AddSolution(AddSolutionRequest request)
        {
            try
            {
                var model = _mapper.Map<SolutionModel>(request);
                model.Author = await _userService
                    .GetUserById(Guid.Parse(User.Claims.FirstOrDefault(u => u.Type == Claims.UserClaim)!.Value));
                await _favorSolutionService.AddFavor(model);
                return RedirectToAction("Index","Home");
            }
            catch
            {
                return RedirectToAction(nameof(AddSolution));
            }
        }
    }
}

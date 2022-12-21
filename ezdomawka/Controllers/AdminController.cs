using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;
        public AdminController(IMapper mapper, AdminService adminService)
        {
            _adminService = adminService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> AddTheme(ThemeRequest request)
        {
            await _adminService.AddTheme(_mapper.Map<ThemeModel>(request));
            return Ok();
        }
    }

}

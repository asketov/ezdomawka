using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.FavorSolution;
using BLL.Models.ViewModels;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class FavorSolutionService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        private readonly AdminService _adminService;

        public FavorSolutionService(DataContext db, IMapper mapper, AdminService adminService)
        {
            _db = db;
            _mapper = mapper;
            _adminService = adminService;
        }

        public async Task AddFavor(SolutionModel model)
        {
            var favor = _mapper.Map<FavorSolution>(model);
            _db.FavorSolutions.Add(favor);
            await _db.SaveChangesAsync();
        }

        public async Task<AddSolutionModel> GetAddSolutionModel()
        {
            var model = new AddSolutionModel();
            model.Subjects = await _adminService.GetSubjectModels();
            model.Themes = await _adminService.GetThemeModels();
            return model;
        }

        public async Task<List<SolutionModel>> GetAllSolutionModels()
        {
            var favorSolutions = await _db.FavorSolutions.Include(x => x.FavorSubjects)
                .ThenInclude(f=>f.Subject).Include(x => x.Theme).Include(d=>d.Author)
                .Select(x=>_mapper.Map<SolutionModel>(x)).ToListAsync();
             return favorSolutions;
        }

        public async Task<int> GetCountSolutions()
        {
            return await _db.FavorSolutions.CountAsync();
        }
    }
}

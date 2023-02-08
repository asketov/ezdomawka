using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Models.Admin;
using BLL.Models.FavorSolution;
using BLL.Models.ViewModels;
using DAL;
using DAL.Entities;
using DAL.Extensions;
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

        public async Task<List<SolutionModel>> GetSolutionModels(int skip, int take, CancellationToken token)
        {
            var favorSolutions = await _db.FavorSolutions.Include(x => x.FavorSubjects)
                .ThenInclude(f=>f.Subject).Include(x => x.Theme).Include(d=>d.Author).Skip(skip).Take(take)
                .ProjectTo<SolutionModel>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync(token);
            return favorSolutions;
        }

        public async Task<List<SolutionModel>> GetSolutionModels(GetSolutionsModel model, CancellationToken token)
        {
            var favorSolutions = await _db.FavorSolutions
                .WithSubjectIdFilter(model.SubjectId).WithThemeIdFilter(model.ThemeId)
                .WithPriceFilter(model.MinPrice, model.MaxPrice).Include(x => x.FavorSubjects)
                .ThenInclude(f => f.Subject).Include(x => x.Theme).Include(d => d.Author)
                .Skip(model.Skip).Take(model.Take)
                .ProjectTo<SolutionModel>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync(token);
            return favorSolutions;
        }

        public async Task<int> GetCountSolutions()
        {
            return await _db.FavorSolutions.CountAsync();
        }

        public async Task<int> GetCountSolutions(GetSolutionsModel model, CancellationToken token)
        {
            return await _db.FavorSolutions
                .WithSubjectIdFilter(model.SubjectId).WithThemeIdFilter(model.ThemeId)
                .CountAsync(token);
        }

        public async Task<SolutionModel> GetSolutionModelById(Guid favorId)
        {
            var favor = await _db.FavorSolutions.Include(x=>x.FavorSubjects).ThenInclude(x => x.Subject).Include(x=>x.Theme).FirstOrDefaultAsync(x => x.Id == favorId);
            return _mapper.Map<SolutionModel>(favor);
        }

        public async Task UpdateFavor(SolutionModel model)
        {
            var favor = _mapper.Map<FavorSolution>(model);
            var dbFavor = await _db.FavorSolutions.AnyAsync(x => x.Id == model.Id);
            if (dbFavor)
            {

            }
            await _db.SaveChangesAsync();
        }
    }
}

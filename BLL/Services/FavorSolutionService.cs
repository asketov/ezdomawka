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
            
            if (favor.FavorSubjects == null ||
                favor.FavorSubjects.Any() == false)
                favor.FavorSubjects = _db.FavorSubject.ToList();
            
            _db.FavorSolutions.Add(favor);
            await _db.SaveChangesAsync();
        }

        public async Task<AddSolutionModel> GetAddSolutionModel()
        {
            var model = new AddSolutionModel();
            var Subjects = (await _adminService.GetSubjectModels()).OrderBy(x => x.Name);
            model.Subjects = Subjects;
            model.Themes = await _adminService.GetThemeModels();
            return model;
        }

        public async Task<List<SolutionModel>> GetSolutionModels(int skip, int take, CancellationToken token)
        {
            var favorSolutions = await _db.FavorSolutions.Where(x => !x.Author.IsBanned)
                .Skip(skip).Take(take)
                .ProjectTo<SolutionModel>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync(token);
            return favorSolutions;
        }

        public async Task<List<SolutionModel>> GetSolutionModels(GetSolutionsModel model, CancellationToken token)
        {
            var favorSolutions = await _db.FavorSolutions
                .WithSubjectIdFilter(model.SubjectId).WithThemeIdFilter(model.ThemeId)
                .WithPriceFilter(model.MinPrice, model.MaxPrice)
                .Where(x => !x.Author.IsBanned)
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
            var favor = await _db.FavorSolutions.Include(x=>x.FavorSubjects).ThenInclude(x => x.Subject)
                .Include(x=>x.Theme).FirstOrDefaultAsync(x => x.Id == favorId);
            return _mapper.Map<SolutionModel>(favor);
        }
        /// <summary>
        /// update if favor exist
        /// </summary>
        public async Task UpdateFavor(SolutionModel model)
        {
            var favor = _mapper.Map<FavorSolution>(model);
            var favorExist = await _db.FavorSolutions.Include(x=>x.FavorSubjects).FirstOrDefaultAsync(x => x.Id == model.Id);
            if (favorExist != null)
            {
                _db.Entry(favorExist).CurrentValues.SetValues(favor);
                favorExist.FavorSubjects = favor.FavorSubjects;
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteFavor(Guid id)
        {
            var favorExist = await _db.FavorSolutions.FirstOrDefaultAsync(x => x.Id == id);
            if (favorExist != null)
            {
                _db.FavorSolutions.Remove(favorExist);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<SubjectModel>> GetFavorSubjects(Guid favorId, int skip, int take)
        {
            var subjects = await _db.Subjects.Where(x => x.FavorSubjects!.Any(x => x.FavorSolution.Id == favorId))
                .Skip(skip).Take(take).ProjectTo<SubjectModel>(_mapper.ConfigurationProvider).ToListAsync();
            return subjects;
        }

        public async Task AddReport(ReportRequest request)
        {
            var model = _mapper.Map<Report>(request);
            _db.Reports.Add(model);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> CheckFavorExist(Guid favorId)
        {
            return await _db.FavorSolutions.AnyAsync(x => x.Id == favorId);
        }

        public async Task<int> GetCountSubjects(Guid favorId)
        {
            return await _db.FavorSubject.CountAsync(x => x.FavorSolutionId == favorId);
        }
    }
}

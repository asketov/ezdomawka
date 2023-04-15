using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Models.Admin;
using BLL.Models.FavorSolution;
using BLL.Models.ViewModels;
using Common.Consts;
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
                !favor.FavorSubjects.Any())
                favor.FavorSubjects = _db.FavorSubject.ToList();
            
            var favorId = _db.FavorSolutions.Add(favor);
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
        public async Task<List<SolutionModel>> GetSolutionModels(GetSolutionsModel model, CancellationToken token)
        {       
            var favorSolutions = await _db.FavorSolutions
                .WithSubjectIdFilter(model.SubjectId).WithThemeIdFilter(model.ThemeId)
                .WithPriceFilter(model.MinPrice, model.MaxPrice)
                .Where(x => !x.Author.IsBanned)
                .OrderByDescending(x => x.Created)
                .ProjectTo<SolutionModel>(_mapper.ConfigurationProvider).Skip(model.Skip)
                .Take(model.Take).AsNoTracking().ToListAsync(token);
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
        public async Task<bool> UpdateFavor(SolutionModel model)
        {
            var favor = _mapper.Map<FavorSolution>(model);
            var favorExist = await _db.FavorSolutions.Include(x=>x.FavorSubjects).FirstOrDefaultAsync(x => x.Id == model.Id);
            if (favorExist != null)
            {
                _db.Entry(favorExist).CurrentValues.SetValues(favor);
                favorExist.FavorSubjects = favor.FavorSubjects;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddRecordToUpdateHistory(Guid userId)
        {
            var mdl = new UpdateFavorHistory()
            {
                UpdateDate = DateTime.UtcNow, AuthorId = userId 
            };
            _db.UpdateFavorHistory.Add(mdl);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> UpdateFavorDate(Guid favorId)
        {
            var favor = await _db.FavorSolutions.FirstOrDefaultAsync(x => x.Id == favorId);
            if (favor != null)
            {
                favor.Created = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
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

        public async Task<IEnumerable<ReportVm>> GetReports(Guid favorId, int skip = 0, int take = 10)
        {
            var favor = await _db.FavorSolutions.Include(x => x.Reports).AsNoTracking().FirstOrDefaultAsync(favor => favor.Id == favorId);

            if (favor == null || favor.Reports == null)
                return Array.Empty<ReportVm>();

            var reports = favor.Reports;
            
            return reports.Skip(skip)
                .Take(take)
                .Select(ban => _mapper.Map<ReportVm>(ban)); 
        }

        public async Task<bool> CheckFavorReportExist(Guid favorReportId)
        {
            return await _db.Reports.AnyAsync(report => report.Id == favorReportId);
        }

        public async Task CleanReports(Guid favorId, Guid? favorReportId = null)
        {
            var favor = await _db.FavorSolutions.FirstOrDefaultAsync(favor => favor.Id == favorId);

            if (favor == null || favor.Reports == null)
                return;

            var reports = favor.Reports;

            if (favorReportId == null)
            {
                _db.RemoveRange(reports);
            }
            else
            {
                var reportToDelete = reports.FirstOrDefault(report => report.Id == favorReportId);

                if (reportToDelete != null) _db.Reports.Remove(reportToDelete);
            }

            await _db.SaveChangesAsync();
        }

        public async Task<int> GetReportsCount(Guid favorId)
        {
            var favor = await _db.FavorSolutions.FirstOrDefaultAsync(favor => favor.Id == favorId);

            if (favor == null || favor.Reports == null)
                return 0;

            return favor.Reports.Count();
        }

        public async Task<bool> FavorsOutOfLimit(Guid userId)
        {
            var count = await _db.FavorSolutions.CountAsync(x => x.AuthorId == userId);
            return (count >= FavorConsts.FavorsLimit) ? true : false; 
        }

        public async Task<int> GetTodayUpdatesFavors(Guid userId)
        {
            var count = await _db.UpdateFavorHistory.CountAsync(x => x.AuthorId == userId 
            && x.UpdateDate >= DateTime.UtcNow.Date && x.UpdateDate < DateTime.UtcNow.Date.AddDays(1));
            return count;
        }
    }
}

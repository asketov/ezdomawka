using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Models.Admin;
using BLL.Models.ViewModels;
using Common.Consts;
using Common.Exceptions.Admin;
using DAL;
using DAL.Entities;
using DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AdminService
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;
        public AdminService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddTheme(ThemeModel model)
        {
            if (await CheckThemeExistByName(model.Name)) throw new ThemeAlreadyExistException();
            var theme = _mapper.Map<Theme>(model);
            _db.Themes.Add(theme);
            await _db.SaveChangesAsync();
        }
        public async Task AddOrUpdateSubject(SubjectModel model)
        {
            if (await CheckSubjectExistByName(model.Name)) throw new SubjectAlreadyExistException();
            var subject = _mapper.Map<Subject>(model);
            if (!await CheckSubjectExist(model.Id))
            {
                _db.Subjects.Add(subject);
                await _db.SaveChangesAsync();
            }
            else
            {
                var subj = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (subj != null)
                {
                    _db.Entry(subj).CurrentValues.SetValues(subject);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> CheckSubjectExist(Guid subjectId)
        {
            return await _db.Subjects.AnyAsync(x => x.Id == subjectId);
        }
        public async Task<bool> CheckThemeExistByName(string name)
        {
            var theme = await _db.Themes.FirstOrDefaultAsync(u => u.Name == name);
            return theme != null;
        }
        public async Task<bool> CheckSubjectExistByName(string name)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(u => u.Name == name);
            return subject != null;
        }

        public async Task<List<SubjectModel>> GetSubjectModels()
        {
            return await _db.Subjects.ProjectTo<SubjectModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<List<ThemeModel>> GetThemeModels()
        {
            return await _db.Themes.ProjectTo<ThemeModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task DeleteSubject(Guid id)
        {
            var subject = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            if (subject != null)
            {
                _db.Subjects.Remove(subject);
                await _db.SaveChangesAsync();
            }
        }
        public async Task DeleteTheme(Guid id)
        {
            var theme = await _db.Themes.FirstOrDefaultAsync(x => x.Id == id);
            if (theme != null)
            {
                _db.Themes.Remove(theme);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<SubjectModel?> GetSubjectModel(Guid id)
        {
            var dbModel = await _db.Subjects.FirstOrDefaultAsync(x => x.Id == id);
            if (dbModel != null)
            {
                var model = _mapper.Map<SubjectModel>(dbModel);
                return model;
            }
            return null;
        }

        public async Task<bool> UserNotAdmin(Guid userId)
        {
            return await _db.Users.AnyAsync(x => x.Id == userId && x.RoleId != Guid.Parse(Roles.SuperAdminId));
        }

        public async Task BanUser(BanRequest request)
        {
            var ban = new Ban()
            {
                BanFrom = DateTime.UtcNow, BanTo = DateTime.UtcNow.AddDays(request.Duration), UserId = request.UserId,
                Reason = request.Reason
            };
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            if (user != null)
            {
                user.IsBanned = true;
                if (user.Bans != null) user.Bans?.Add(ban);
                else user.Bans = new List<Ban>() { ban };
                await _db.SaveChangesAsync();
            }
        }

        public async Task GetUsersByRequest(UserPanelRequest request, CancellationToken token)
        {
            var users = await _db.Users.WithEmailFilter(request.Email).WithNickFilter(request.Nick)
                .Skip(request.Skip).Take(request.Take).AsNoTracking().ToListAsync(token);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using BLL.Models.ViewModels;
using Common.Exceptions.Admin;
using DAL;
using DAL.Entities;
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
        public async Task AddSubject(SubjectModel model)
        {
            if (await CheckSubjectExistByName(model.Name)) throw new SubjectAlreadyExistException();
            var subject = _mapper.Map<Subject>(model);
            _db.Subjects.Add(subject);
            await _db.SaveChangesAsync();
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
            return await _db.Subjects.Select(x=>_mapper.Map<SubjectModel>(x)).ToListAsync();
        }
        public async Task<List<ThemeModel>> GetThemeModels()
        {
            return await _db.Themes.Select(x => _mapper.Map<ThemeModel>(x)).ToListAsync();
        }
    }
}

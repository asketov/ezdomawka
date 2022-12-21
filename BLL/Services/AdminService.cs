using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.Admin;
using DAL;
using DAL.Entities;

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
            var theme = _mapper.Map<Theme>(model);
            _db.Themes.Add(theme);
            await _db.SaveChangesAsync();
        }
        public async Task AddSubject(SubjectRequest request)
        {
            var subject = _mapper.Map<Subject>(request);
            _db.Subjects.Add(subject);
            await _db.SaveChangesAsync();
        }
    }
}

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

        public FavorSolutionService(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddFavor(AddSolutionModel model)
        {
            var favor = _mapper.Map<FavorSolution>(model);
            _db.FavorSolutions.Add(favor);
            await _db.SaveChangesAsync();
        }

        public async Task<AddSolutionModel> GetAddSolutionModel()
        {
            var model = new AddSolutionModel();
            model.Subjects = await _db.Subjects.ToListAsync();
            model.Themes = await _db.Themes.ToListAsync();
            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models.FavorSolution;
using DAL;
using DAL.Entities;

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
    }
}

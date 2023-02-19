using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;

namespace BLL.Services
{
    public class HomeService
    {
        private readonly DataContext _db;
        public HomeService(DataContext db)
        {
            _db = db;
        }

        public async Task AddSuggestion(Suggestion model)
        {
            _db.Suggestions.Add(model);
            await _db.SaveChangesAsync();
        }
    }
}

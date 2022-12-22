using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Models.FavorSolution
{
    public class AddSolutionModel
    {
        
        public string Text { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public User Author { get; set; } = null!;
        public IEnumerable<Subject> Subjects { get; set; } = null!;
        public IEnumerable<Theme> Themes { get; set; } = null!;
    }
}

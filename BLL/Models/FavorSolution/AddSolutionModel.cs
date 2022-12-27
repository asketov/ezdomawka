using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Admin;

namespace BLL.Models.FavorSolution
{
    public class AddSolutionModel
    {
        public string Text { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public DateTimeOffset Created { get; set; }
        public IEnumerable<SubjectModel> Subjects { get; set; } = null!;
        public IEnumerable<ThemeModel> Themes { get; set; } = null!;
    }
}

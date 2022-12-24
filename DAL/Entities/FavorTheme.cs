using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FavorTheme
    {
        public Guid FavorSolutionId { get; set; }
        public FavorSolution FavorSolution { get; set; } = null!;
        public Guid ThemeId { get; set; }
        public Theme Theme { get; set; } = null!;
    }
}

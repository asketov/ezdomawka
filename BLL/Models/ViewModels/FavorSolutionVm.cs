using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class FavorSolutionVm
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string Nick { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public IEnumerable<SubjectVm> Subjects { get; set; } = null!;
        public IEnumerable<ThemeVm> Themes { get; set; } = null!;
    }
}

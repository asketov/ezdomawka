using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class IndexVm
    {
        public IEnumerable<ThemeVm> ThemeVms { get; set; } = null!;
        public IEnumerable<SubjectVm> SubjectVms { get; set; } = null!;
        public FavorsWithPaginationVm FavorsWithPaginationVm { get; set; } = null!;
    }
}

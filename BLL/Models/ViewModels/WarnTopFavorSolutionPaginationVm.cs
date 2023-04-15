using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class WarnTopFavorSolutionPaginationVm
    {
        public IEnumerable<WarnTopFavorSolutionVm> WarnTopFavorSolutions { get; set; } = null!;
        public int Count { get; set; } = 0;
    }
}

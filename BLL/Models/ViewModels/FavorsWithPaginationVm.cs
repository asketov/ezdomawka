using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class FavorsWithPaginationVm
    {
        public IEnumerable<FavorSolutionVm> FavorSolutionVms { get; set; } = null!;
        public int CountFavorSolutions { get; set; }
    }
}

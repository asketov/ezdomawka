using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class MyFavorsVm
    {
        public IEnumerable<FavorSolutionVm> favors { get; set; } = null!;
        public int RemainUpdates { get; set; }
    }
}

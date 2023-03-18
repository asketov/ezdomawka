using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class UserPanelVm
    {
        public IEnumerable<UserVm> UserVms { get; set; } = null!;
        public int CountUsersByFilters { get; set; }
    }
}

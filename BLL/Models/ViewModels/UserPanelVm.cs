using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class UserPanelVm
    {
        [Required]
        public IEnumerable<UserVm> UserVms { get; set; } = null!;
        
        [Required]
        public int CountUsersByFilters { get; set; }
    }
}

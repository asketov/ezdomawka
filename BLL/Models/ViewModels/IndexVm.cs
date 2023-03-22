using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class IndexVm
    {
        [Required]
        public IEnumerable<ThemeVm> ThemeVms { get; set; } = null!;
        
        [Required]
        public IEnumerable<SubjectVm> SubjectVms { get; set; } = null!;
        
        [Required]
        public FavorsWithPaginationVm FavorsWithPaginationVm { get; set; } = null!;
    }
}

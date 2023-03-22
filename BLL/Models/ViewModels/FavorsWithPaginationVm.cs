using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class FavorsWithPaginationVm
    {
        [Required]
        public IEnumerable<FavorSolutionVm> FavorSolutionVms { get; set; } = null!;
        
        [Required]
        public int CountFavorSolutions { get; set; }
    }
}

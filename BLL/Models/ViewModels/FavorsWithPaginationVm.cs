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
        public uint CountFavorSolutions { get; set; }
        
        [Required]
        public uint FavorSolutionsOffset { get; set; }

        public PaginationConfiguration PaginationConfig => new PaginationConfiguration()
            { CountFavorSolutions = CountFavorSolutions, FavorSolutionsOffset = FavorSolutionsOffset };
        
        public class PaginationConfiguration
        {
            [Required]
            public uint CountFavorSolutions { get; set; }
        
            [Required]
            public uint FavorSolutionsOffset { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FavorSolution
{
    public class GetSolutionsRequest
    {
        [Required]
        public Guid ThemeId { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        [Range(0, 200)]
        public int Skip { get; set; }
        [Required]
        [Range(0, 200)]
        public int Take { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FavorSolution
{
    public class FindFavorsRequest
    {
        [Required]
        public Guid ThemeId { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
    }
}

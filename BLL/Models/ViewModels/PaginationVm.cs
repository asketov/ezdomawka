using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class PaginationVm
    {
        [Required]
        public int Begin { get; set; }
        
        [Required]
        public int End { get; set; }
        
        [Required]
        public int CountSolutions { get; set; }
    }
}

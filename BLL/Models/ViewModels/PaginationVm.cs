using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class PaginationVm
    {
        public int Begin { get; set; }
        public int End { get; set; }
        public int CountSolutions { get; set; }
    }
}

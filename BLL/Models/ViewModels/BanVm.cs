using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class BanVm
    {
        public DateTime BanTo { get; set; }
        public string Reason { get; set; } = null!;
    }
}

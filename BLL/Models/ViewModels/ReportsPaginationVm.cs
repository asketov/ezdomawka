using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class ReportsPaginationVm
    {
        public IEnumerable<ReportVm> Reports { get; set; } = null!;
        public int Count { get; set; }
        public Guid FavorId { get; set; }
    }
}

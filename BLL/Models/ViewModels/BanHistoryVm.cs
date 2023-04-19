using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class BanHistoryVm
    {
        public IEnumerable<BanVm> Bans { get; set; } = null!;
        public int Count { get; set; }
        public Guid UserId { get; set; }
    }
}

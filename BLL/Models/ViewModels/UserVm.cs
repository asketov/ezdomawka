using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string Nick { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsBanned { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Auth
{
    public class ChangePasswordModel
    {
        public string PasswordHash { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Auth
{
    public class RegisterModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nick { get; set; } = null!;
    }
}

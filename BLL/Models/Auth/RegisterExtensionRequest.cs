using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Auth
{
    public class RegisterExtensionRequest : RegisterRequest
    {
        [Required] 
        [Range(100000, 999999)]
        public int ConfirmCode { get; set; }
    }
}

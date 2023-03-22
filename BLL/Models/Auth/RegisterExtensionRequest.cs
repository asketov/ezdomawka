using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.Auth
{
    public class RegisterExtensionRequest : RegisterRequest
    {
        [Required] 
        [Range(ConfirmCodeConfiguration.MinValue, ConfirmCodeConfiguration.MaxValue)]
        public int ConfirmCode { get; set; }
    }
}

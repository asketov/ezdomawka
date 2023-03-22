using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.Auth
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Поле является обязательным")]
        public string PasswordHash { get; set; } = null!;
        
        
        [Required]
        [EmailAddress]
        [MaxLength(MailConfiguration.MaxLength, ErrorMessage = "Почта должна быть меньше {1} символов")]
        public string Email { get; set; } = null!;
    }
}

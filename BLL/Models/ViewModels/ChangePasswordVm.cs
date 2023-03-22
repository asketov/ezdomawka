using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.ViewModels
{
    public class ChangePasswordVm
    {
        [DataType(DataType.Password)]
        [StringLength(PasswordConfiguration.MaxLength, MinimumLength = PasswordConfiguration.MinLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; } = null!;

        
        [DataType(DataType.Password)]
        [StringLength(PasswordConfiguration.MaxLength, MinimumLength = PasswordConfiguration.MinLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;
        
        
        [Required]
        public Guid Code { get; set; }
    }
}

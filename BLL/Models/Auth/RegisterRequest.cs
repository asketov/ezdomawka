using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Поле является обязательным")]
        [EmailAddress(ErrorMessage = "Поле должно являться почтой")]
        [MaxLength(100, ErrorMessage = "Почта должна быть меньше 100 символов")]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Поле является обязательным")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Nick { get; set; } = null!;
    }
}

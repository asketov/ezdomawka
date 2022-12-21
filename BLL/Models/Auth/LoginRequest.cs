using System.ComponentModel.DataAnnotations;


namespace BLL.Models.Auth
{
    public class LoginRequest
    {
        [EmailAddress(ErrorMessage = "Поле должно являться почтой")]
        [MaxLength(100, ErrorMessage = "Почта должна быть меньше 100 символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Password { get; set; } = null!;
    }
}

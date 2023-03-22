using System.ComponentModel.DataAnnotations;
using ModelsConfiguration;

namespace BLL.Models.UserModels
{
    public class UserModel
    {
        [Required]
        public Guid Id { get; set; }
        
        
        [EmailAddress(ErrorMessage = "Поле должно являться почтой")]
        [MaxLength(MailConfiguration.MaxLength, ErrorMessage = "Почта должна быть меньше {1} символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Email { get; set; } = null!;
    }
}

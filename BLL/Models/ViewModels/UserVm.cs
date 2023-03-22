using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.ViewModels
{
    public class UserVm
    {
        [Required]
        public Guid Id { get; set; }
        
        
        [StringLength(NickConfiguration.MaxLength, MinimumLength = NickConfiguration.MinLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Nick { get; set; } = null!;
        
        
        [EmailAddress(ErrorMessage = "Поле должно являться почтой")]
        [MaxLength(MailConfiguration.MaxLength, ErrorMessage = "Почта должна быть меньше {1} символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Email { get; set; } = null!;
        
        
        [Required]
        public bool IsBanned { get; set; }
    }
}

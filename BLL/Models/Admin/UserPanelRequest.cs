using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.Admin
{
    public class UserPanelRequest
    {
        [StringLength(NickConfiguration.MaxLength, MinimumLength = NickConfiguration.MinLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        public string? Nick { get; set; }
        
        
        [EmailAddress(ErrorMessage = "Поле должно являться почтой")]
        [MaxLength(MailConfiguration.MaxLength, ErrorMessage = "Почта должна быть меньше {1} символов")]
        public string? Email { get; set; }
        
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Consts;
using ModelsConfiguration;

namespace BLL.Models.Admin
{
    public class BanRequest
    {
        [Required]
        [StringLength(BanConfiguration.MaxReasonTextLenght, 
            MinimumLength = BanConfiguration.MinReasonTextLenght, ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        public string Reason { get; set; } = null!;
        
        
        [Required]
        public Guid UserId { get; set; }
        
        
        [Required]
        [Range(BanConfiguration.MinDuration, BanConfiguration.MaxDuration)]
        public int Duration { get; set; }
    }
}

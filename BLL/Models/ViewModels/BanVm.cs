using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.ViewModels
{
    public class BanVm
    {
        [Required]
        public DateTime BanTo { get; set; }
        
        [Required]
        public DateTime BanFrom { get; set; }
        
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        [StringLength(BanConfiguration.MaxReasonTextLenght, 
            MinimumLength = BanConfiguration.MinReasonTextLenght, ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        public string Reason { get; set; } = null!;

        public bool IsActual { get; set; } = true;
    }
}

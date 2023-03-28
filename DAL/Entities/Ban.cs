using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace DAL.Entities
{
    [Table("Bans")]
    public class Ban
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public DateTime BanFrom { get; set; }
        
        [Required]
        public DateTime BanTo { get; set; }
        
        [Required]
        [MaxLength(BanConfiguration.MaxReasonTextLenght)]
        [MinLength(BanConfiguration.MinReasonTextLenght)]
        public string Reason { get; set; } = null!;
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public virtual User User { get; set; } = null!;

        public bool IsActual { get; set; } = true;
    }
}

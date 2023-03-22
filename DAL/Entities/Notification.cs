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
    [Table("Notification")]
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(NotificationConfiguration.MaxTextLength)]
        [MinLength(NotificationConfiguration.MinTextLength)]
        [Required]
        public string Text { get; set; } = null!;
        
        [Required]
        public Guid UserId { get; set; }
        
        [Required]
        public User User { get; set; } = null!;
    }
}

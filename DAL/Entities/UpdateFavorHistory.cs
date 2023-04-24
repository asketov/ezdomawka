using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("UpdateFavorHistory")]
    public class UpdateFavorHistory
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public DateTime UpdateDate { get; set; }
        
        [Required]
        public Guid AuthorId { get; set; }
        
        [Required]
        public virtual User Author { get; set; } = null!;
    }
}

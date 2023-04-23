using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace DAL.Entities
{
    [Table("Themes")]
    public class Theme
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(ThemeConfiguration.MaxNameLength)]
        [MinLength(ThemeConfiguration.MinNameLength)]
        [Required]
        public string Name { get; set; } = null!;
        
        //May by not required
        [Required]
        public virtual ICollection<FavorSolution> FavorSolutions { get; set; } = null!;
        [Required]
        public Guid InstituteId { get; set; }
        [Required]
        public virtual Institute Institute { get; set; } = null!;
    }
}

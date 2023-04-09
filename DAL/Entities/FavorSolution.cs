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
    [Table("FavorSolutions")]
    public class FavorSolution
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public DateTimeOffset Created { get; set; }
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int Price { get; set; }
        
        [MaxLength(SolutionConfiguration.TextMaxLength)]
        [MinLength(SolutionConfiguration.TextMinLength)]
        public string Text { get; set; } = null!;
        
        [MaxLength(ConnectionConfiguration.MaxConnectionLength)]
        [MinLength(ConnectionConfiguration.MinConnectionLength)]
        public string Connection { get; set; } = null!;
        
        [Required]
        public virtual User Author { get; set; } = null!;
        
        [Required]
        public Guid AuthorId { get; set; }
        
        [Required]
        public virtual ICollection<FavorSubject> FavorSubjects { get; set; } = null!;
        
        [AllowNull]
        public virtual ICollection<Report>? Reports { get; set; }
        
        
        [AllowNull]
        public virtual ICollection<Review>? Reviews { get; set; }
        
        [Required]
        public Guid ThemeId { get; set; }
        
        [Required]
        public virtual Theme Theme { get; set; } = null!;
    }
}

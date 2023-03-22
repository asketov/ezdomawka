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
    [Table("Subjects")]
    public class Subject
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(SubjectConfiguration.MaxNameLength)]
        [MinLength(SubjectConfiguration.MinNameLength)]
        [Required]
        public string Name { get; set; } = null!;
        
        [AllowNull]
        public virtual ICollection<FavorSubject>? FavorSubjects { get; set; }
    }
}

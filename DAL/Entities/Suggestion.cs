using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelsConfiguration;

namespace DAL.Entities
{
    [Table("Suggestions")]
    [Index("CreationDate", IsUnique = false)]
    public class Suggestion
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(SuggestionConfiguration.MaxTextLength)]
        [MinLength(SuggestionConfiguration.MinTextLength)]
        public string Text { get; set; } = null!;
        
        [AllowNull]
        [MaxLength(ConnectionConfiguration.MaxConnectionLength)]
        [MinLength(ConnectionConfiguration.MinConnectionLength)]
        public string? Connection { get; set; } 
        
        [Required]
        public bool IsActual { get; set; }
        
        [Required] 
        public DateTime CreationDate { get; set; }
    }
}

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
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(ReviewConfiguration.MaxTextLength)]
        [MinLength(ReviewConfiguration.MinTextLength)]
        [Required]
        public string Text { get; set; } = null!;
        
        [Required]
        public Guid FavorId { get; set; }
        [Required]
        public FavorSolution Favor { get; set; } = null!;
        
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public User Author { get; set; } = null!;

    }
}

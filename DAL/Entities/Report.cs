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
    [Table("Reports")]
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        
        [MaxLength(ReportConfiguration.MaxReasonLength)]
        [Required]
        public string Reason { get; set; } = null!;
        
        [Required]
        public Guid FavorSolutionId { get; set; }
        
        [Required]
        public virtual FavorSolution FavorSolution { get; set; } = null!;
        [Required]
        public DateTime Created { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("FavoriteSubjects")]
    public class FavorSubject
    {
        [Required]
        public Guid FavorSolutionId { get; set; }
        
        [Required]
        public FavorSolution FavorSolution { get; set; } = null!;
        
        [Required]
        public Guid SubjectId { get; set; }
        
        [Required]
        public Subject Subject { get; set; } = null!;
    }
}

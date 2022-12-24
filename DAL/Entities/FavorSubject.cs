using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FavorSubject
    {
        public Guid FavorSolutionId { get; set; }
        public FavorSolution FavorSolution { get; set; } = null!;
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
    }
}

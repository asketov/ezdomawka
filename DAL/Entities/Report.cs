using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public string Reason { get; set; } = null!;
        public Guid FavorSolutionId { get; set; }
        public virtual FavorSolution FavorSolution { get; set; } = null!;
    }
}

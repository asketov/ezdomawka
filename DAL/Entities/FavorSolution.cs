using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class FavorSolution
    {
        public Guid Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Price { get; set; } 
        public double Rating { get; set; }
        public string Text { get; set; } = null!;
        public virtual User Author { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; } = null!;
    }
}

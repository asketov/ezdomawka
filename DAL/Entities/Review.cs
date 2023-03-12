using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public Guid FavorId { get; set; }
        public FavorSolution Favor { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public User Author { get; set; } = null!;

    }
}

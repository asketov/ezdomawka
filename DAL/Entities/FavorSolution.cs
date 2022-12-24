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
        public string Text { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public virtual User Author { get; set; } = null!;
        public Guid AuthorId { get; set; }
        public virtual ICollection<FavorSubject> FavorSubjects { get; set; } = null!;
        public virtual ICollection<FavorTheme> FavorThemes { get; set; } = null!;
    }
}

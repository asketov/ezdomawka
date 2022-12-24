using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<FavorSubject>? FavorSubjects { get; set; }

    }
}

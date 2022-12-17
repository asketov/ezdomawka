using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public ICollection<Subject> Subjects { get; set; } = null!;
    }
}

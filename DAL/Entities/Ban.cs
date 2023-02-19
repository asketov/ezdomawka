using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Ban
    {
        public Guid Id { get; set; }
        public DateTime BanFrom { get; set; }
        public DateTime BanTo { get; set; }
        public string Reason { get; set; } = null!;
        public virtual User User { get; set; } = null!;

    }
}

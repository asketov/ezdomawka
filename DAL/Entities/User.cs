using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string Nick { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsBanned { get; set; }
        public virtual ICollection<Ban>? Bans { get; set; }
        public virtual ICollection<FavorSolution>? FavorSolutions { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; } = null!;
    }
}

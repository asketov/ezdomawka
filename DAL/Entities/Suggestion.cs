using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Suggestion
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public string? Connection { get; set; } 
        public bool IsActual { get; set; }
    }
}

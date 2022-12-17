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
    }
}

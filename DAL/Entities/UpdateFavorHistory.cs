using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UpdateFavorHistory
    {
        public Guid Id { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid FavorId { get; set; }
        public virtual FavorSolution Favor { get; set; } = null!;


    }
}

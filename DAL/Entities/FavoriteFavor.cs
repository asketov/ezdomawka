using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("FavoriteFavors")]
    public class FavoriteFavor : FavorSolution
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public virtual User User { get; set; } = null!;
    }
}

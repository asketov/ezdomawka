using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Consts;

namespace BLL.Models.Admin
{
    public class BanRequest
    {
        [Required]
        public string Reason { get; set; } = null!;
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [Range(0,10000000)]
        public int Duration { get; set; }


    }
}

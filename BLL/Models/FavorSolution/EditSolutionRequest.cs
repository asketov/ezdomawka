using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FavorSolution
{
    public class EditSolutionRequest : AddSolutionRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}

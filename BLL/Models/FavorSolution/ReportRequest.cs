using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.FavorSolution
{
    public class ReportRequest
    {
        [MaxLength(ReportConfiguration.MaxReasonLength, ErrorMessage = "Слишком много символов")]
        [Required(ErrorMessage = "Поле причина обязательно")]
        public string Reason { get; set; } = null!;
        
        [Required]
        public Guid FavorSolutionId { get; set; }

    }
}

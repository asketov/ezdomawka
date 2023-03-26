using System.ComponentModel.DataAnnotations;
using ModelsConfiguration;

namespace BLL.Models.ViewModels;

public class ReportVm
{
    [MaxLength(ReportConfiguration.MaxReasonLength, ErrorMessage = "Слишком много символов")]
    [Required(ErrorMessage = "Поле причина обязательно")]
    public string Reason { get; set; } = null!;
}
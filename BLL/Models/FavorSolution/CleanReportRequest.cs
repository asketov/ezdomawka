using System.ComponentModel.DataAnnotations;

namespace BLL.Models.FavorSolution;

public class CleanReportRequest
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid FavorId { get; set; }
    [Required] public Guid FavorReportId { get; set; }
}
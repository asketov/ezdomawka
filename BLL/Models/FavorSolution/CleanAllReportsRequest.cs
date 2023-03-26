using System.ComponentModel.DataAnnotations;

namespace BLL.Models.FavorSolution;

    public class CleanAllReportsRequest
    {
        [Required] public Guid UserId { get; set; }
        [Required] public Guid FavorId { get; set; }
    }
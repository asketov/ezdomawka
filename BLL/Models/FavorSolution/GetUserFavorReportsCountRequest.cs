using System.ComponentModel.DataAnnotations;

namespace BLL.Models.FavorSolution;

public class GetUserFavorReportsCountRequest
{
    [Required] public Guid FavorId { get; set; }
}
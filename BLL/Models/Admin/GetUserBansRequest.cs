using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Admin;

public class GetUserBansRequest
{
    [Required]
    public Guid UserId { get; set; }
    
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 10;
}
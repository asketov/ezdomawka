using System.ComponentModel.DataAnnotations;


namespace BLL.Models.Auth
{
    public class LoginRequest
    {
        [EmailAddress]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [MinLength(5)]
        [MaxLength(50)]
        [Required]
        public string Password { get; set; } = null!;
    }
}

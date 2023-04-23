using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace DAL.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [EmailAddress]
        [MaxLength(MailConfiguration.MaxLength)]
        [Required]
        public string Email { get; set; } = null!;
        
        [MaxLength(NickConfiguration.MaxLength)]
        [MinLength(NickConfiguration.MinLength)]
        [Required]
        public string Nick { get; set; } = null!;
        
        [Required]
        public string PasswordHash { get; set; } = null!;
        
        [Required]
        public bool IsBanned { get; set; }
        
        [AllowNull]
        public virtual ICollection<Ban>? Bans { get; set; }
        
        [AllowNull]
        public virtual ICollection<FavorSolution>? FavorSolutions { get; set; }
        
        [AllowNull]
        public virtual ICollection<FavoriteFavor>? FavoriteFavors { get; set; }
        
        [AllowNull]
        public virtual ICollection<Review>? Reviews { get; set; }
        
        [AllowNull]
        public virtual ICollection<Notification>? Notifications { get; set; }
        
        [Required]
        public Guid RoleId { get; set; }
        
        [Required]
        public virtual Role Role { get; set; } = null!;
        [AllowNull]
        public virtual ICollection<UpdateFavorHistory>? UpdateFavorHistory { get; set; } = null!;
        [Required]
        public Guid InstituteId { get; set; }
        [Required]
        public virtual Institute Institute { get; set; } = null!;

    }
}

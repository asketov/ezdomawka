using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.Admin
{
    public class SubjectModel 
    {
        [Required]
        public Guid Id { get; set; }
        
        
        [StringLength(SubjectConfiguration.MaxNameLength, MinimumLength = SubjectConfiguration.MinNameLength, ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [Required]
        public string Name { get; set; } = null!;
    }
}

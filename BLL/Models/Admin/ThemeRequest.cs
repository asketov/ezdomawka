using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.Admin
{
    public class ThemeRequest
    {
        [Required]
        [StringLength(ThemeConfiguration.MaxNameLength, MinimumLength = ThemeConfiguration.MinNameLength, ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        public string Name { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class SuggestionVm
    {
        [MaxLength(100, ErrorMessage = "Длина должна быть меньше 100 символов")]
        public string? Connection { get; set; } = null!;
        [Required(ErrorMessage = "Введите текст")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Текст должен быть от 1 до 500 символов")]
        public string Text { get; set; } = null!;
    }
}

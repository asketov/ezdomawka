using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class AddSolutionVm
    {
        [Required(ErrorMessage = "Введите текст заявки")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Текст должен быть меньше 200 символов")]
        public string Text { get; set; } = null!;
        [Range(0, 20000, ErrorMessage = "Цена должна быть между 0 и 20000")]
        [Required(ErrorMessage = "Введите цену")]
        public string Price { get; set; } = null!;
        [Required(ErrorMessage = "Введите контакты для связи")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        public string Connection { get; set; } = null!;
        public IEnumerable<SubjectVm> Subjects { get; set; } = null!;
        public IEnumerable<ThemeVm> Themes { get; set; } = null!;
    }
}

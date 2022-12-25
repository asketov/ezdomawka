using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.ViewModels;
using DAL.Entities;

namespace BLL.Models.FavorSolution
{
    public class AddSolutionRequest
    {
        [Required(ErrorMessage = "Введите текст заявки")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Текст должен быть меньше 200 символов")]
        public string Text { get; set; } = null!;
        [Range(0, 2000000)]
        [Required(ErrorMessage = "Введите цену")]
        public string Price { get; set; } = null!;
        [Required(ErrorMessage = "Введите контакты для связи")] 
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        public string Connection { get; set; } = null!;
        [Required(ErrorMessage = "Введите предмет")]
        public IEnumerable<SubjectVm> Subjects { get; set; } = null!;
        [Required(ErrorMessage = "Введите тему")]
        public IEnumerable<ThemeVm> Themes { get; set; } = null!;
    }
}

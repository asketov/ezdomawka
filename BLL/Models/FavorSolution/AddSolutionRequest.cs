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
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Текст должен быть меньше 200 символов и не быть пустым")]
        public string Text { get; set; } = null!;
        [Range(0, 2000000)]
        [Required(ErrorMessage = "Введите цену")]
        public string Price { get; set; } = null!;
        [Required(ErrorMessage = "Введите контакты для связи")] 
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина должна быть от 5 до 50 символов")]
        [DataType(DataType.Url, ErrorMessage = "Связь должна являться одной активной ссылкой")]
        public string Connection { get; set; } = null!;
        [Required(ErrorMessage = "Введите предмет")]
        public IEnumerable<SubjectVm> Subjects { get; set; } = null!;
        [Required(ErrorMessage = "Введите тему")]
        public ThemeVm Theme { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Admin;
using ModelsConfiguration;

namespace BLL.Models.FavorSolution
{
    public class AddSolutionModel
    {
        [Required(ErrorMessage = "Введите текст заявки")]
        [StringLength(SolutionConfiguration.TextMaxLength, MinimumLength = SolutionConfiguration.TextMinLength,
            ErrorMessage = "Текст должен быть меньше {1} символов и не быть пустым")]
        public string Text { get; set; } = null!;
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        [Required(ErrorMessage = "Введите цену")]
        public string Price { get; set; } = null!;
        
        
        [Required(ErrorMessage = "Введите контакты для связи")] 
        [StringLength(ConnectionConfiguration.MaxConnectionLength, MinimumLength = ConnectionConfiguration.MinConnectionLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [DataType(DataType.Url, ErrorMessage = "Связь должна являться одной активной ссылкой")]
        public string Connection { get; set; } = null!;
        
        
        [Required]
        public DateTime Created { get; set; }
        
        
        [Required]
        public IEnumerable<SubjectModel> Subjects { get; set; } = null!;
        
        
        [Required]
        public IEnumerable<ThemeModel> Themes { get; set; } = null!;
    }
}

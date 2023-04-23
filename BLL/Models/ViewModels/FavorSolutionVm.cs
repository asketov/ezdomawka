
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.ViewModels
{
    public class FavorSolutionVm
    {
        [Required]
        public Guid AuthorId { get; set; }
        
        
        [Required]
        public Guid Id { get; set; }
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        [Required]
        public int Price { get; set; }      
        
        
        [StringLength(NickConfiguration.MaxLength, MinimumLength = NickConfiguration.MinLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public string Nick { get; set; } = null!; 
        
        
        [Required(ErrorMessage = "Введите текст заявки")]
        [StringLength(SolutionConfiguration.TextMaxLength, MinimumLength = SolutionConfiguration.TextMinLength,
            ErrorMessage = "Текст должен быть меньше {1} символов и не быть пустым")]
        public string Text { get; set; } = null!;
        
        
        [Required(ErrorMessage = "Введите контакты для связи")] 
        [StringLength(ConnectionConfiguration.MaxConnectionLength, MinimumLength = ConnectionConfiguration.MinConnectionLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [DataType(DataType.Url, ErrorMessage = "Связь должна являться одной активной ссылкой")]
        public string Connection { get; set; } = null!;
        
        
        [Required]
        public IEnumerable<SubjectVm> Subjects { get; set; } = null!;
        
        
        [Required]
        public ThemeVm Theme { get; set; } = null!; 
        
        
        [Required]
        public DateTime Created { get; set; }
    }
}

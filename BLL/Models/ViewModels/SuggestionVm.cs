using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.ViewModels
{
    public class SuggestionVm
    {
        [Required(ErrorMessage = "Введите контакты для связи")] 
        [StringLength(ConnectionConfiguration.MaxConnectionLength, MinimumLength = ConnectionConfiguration.MinConnectionLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [DataType(DataType.Url, ErrorMessage = "Связь должна являться одной активной ссылкой")]
        public string? Connection { get; set; } = null!;
        
        
        [Required(ErrorMessage = "Введите текст")]
        [StringLength(SuggestionConfiguration.MaxTextLength, MinimumLength = SuggestionConfiguration.MinTextLength, ErrorMessage = "Текст должен быть от {1} до {2} символов")]
        public string Text { get; set; } = null!;
    }
}

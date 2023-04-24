using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ModelsConfiguration;

namespace BLL.Models.ViewModels;

public class AdminSuggestionVm
{
    [NotNull] public UserVm User { get; set; } = null!;

    [Required(ErrorMessage = "Введите контакты для связи")]
    [StringLength(ConnectionConfiguration.MaxConnectionLength,
        MinimumLength = ConnectionConfiguration.MinConnectionLength,
        ErrorMessage = "Длина должна быть от {1} до {2} символов")]
    [DataType(DataType.Url, ErrorMessage = "Связь должна являться одной активной ссылкой")]
    public string? Connection { get; set; } = null!;


    [Required(ErrorMessage = "Введите текст")]
    [StringLength(SuggestionConfiguration.MaxTextLength, MinimumLength = SuggestionConfiguration.MinTextLength,
        ErrorMessage = "Текст должен быть от {1} до {2} символов")]
    public string Text { get; set; } = null!;
}
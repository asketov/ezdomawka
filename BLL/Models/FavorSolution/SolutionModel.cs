using System.ComponentModel.DataAnnotations;
using BLL.Models.Admin;
using DAL.Entities;
using ModelsConfiguration;

namespace BLL.Models.FavorSolution
{
    public class SolutionModel
    {
        [Required]
        public Guid Id { get; set; }
        
        
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public User Author { get; set; } = null!;
        
        
        [Required]
        [StringLength(SolutionConfiguration.TextMaxLength, MinimumLength = SolutionConfiguration.TextMinLength,
            ErrorMessage = "Текст должен быть меньше {1} символов и не быть пустым")]
        public string Text { get; set; } = null!;
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        [Required]
        public string Price { get; set; } = null!;
        
        
        [Required(ErrorMessage = "Введите контакты для связи")] 
        [StringLength(ConnectionConfiguration.MaxConnectionLength, MinimumLength = ConnectionConfiguration.MinConnectionLength, 
            ErrorMessage = "Длина должна быть от {1} до {2} символов")]
        [DataType(DataType.Url, ErrorMessage = "Связь должна являться одной активной ссылкой")]
        public string Connection { get; set; } = null!;
        
        
        [Required]
        public DateTimeOffset Created { get; set; }
        
        
        [Required]
        public IEnumerable<SubjectModel> Subjects { get; set; } = null!;
        [Required]
        public ThemeModel Theme { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities;
using ModelsConfiguration;

namespace BLL.Models.ViewModels;

public class WarnTopFavorSolutionVm 
{
    public Guid Id { get; set; }
        
    [Required]
    public DateTimeOffset Created { get; set; }
        
    [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
    public int Price { get; set; }
        
    [MaxLength(SolutionConfiguration.TextMaxLength)]
    [MinLength(SolutionConfiguration.TextMinLength)]
    public string Text { get; set; } = null!;
        
    [MaxLength(ConnectionConfiguration.MaxConnectionLength)]
    [MinLength(ConnectionConfiguration.MinConnectionLength)]
    public string Connection { get; set; } = null!;
        
    [Required]
    public virtual User Author { get; set; } = null!;
        
    [Required]
    public Guid AuthorId { get; set; }

    [Required]
    public Guid ThemeId { get; set; }
        
    [Required]
    public virtual Theme Theme { get; set; } = null!;
    
    [Required] 
    public int ReportCount { get; set; } = 0;
}
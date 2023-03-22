using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ModelsConfiguration;

namespace BLL.Models.FavorSolution
{
    public class GetSolutionsRequest
    {
        [AllowNull]
        public Guid? ThemeId { get; set; }
        
        
        [AllowNull]
        public Guid? SubjectId { get; set; }
        
        
        [Required]
        [Range(SolutionConfiguration.GetRequest.MinSkip, SolutionConfiguration.GetRequest.MaxSkip)]
        public int Skip { get; set; }
        
        
        [Required]
        [Range(SolutionConfiguration.GetRequest.MinTake, SolutionConfiguration.GetRequest.MaxTake)]
        public int Take { get; set; }
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int MinPrice { get; set; }
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int MaxPrice { get; set; }

    }
}

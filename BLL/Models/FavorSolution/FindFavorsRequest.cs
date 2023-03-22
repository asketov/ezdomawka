using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsConfiguration;

namespace BLL.Models.FavorSolution
{
    public class FindFavorsRequest
    {
        [AllowNull]
        public Guid? ThemeId { get; set; }
        
        
        [AllowNull]
        public Guid? SubjectId { get; set; }
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int MinPrice { get; set; }
        
        
        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int MaxPrice { get; set; }
    }
}

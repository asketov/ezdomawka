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
    public class GetSolutionsModel
    {
        [AllowNull]
        public Guid? ThemeId { get; set; }
        
        
        [AllowNull]
        public Guid? SubjectId { get; set; }


        [Range(SolutionConfiguration.GetRequest.MinSkip, SolutionConfiguration.GetRequest.MaxSkip)]
        public int Skip { get; set; } = 0;


        [Range(SolutionConfiguration.GetRequest.MinTake, SolutionConfiguration.GetRequest.MaxTake)]
        public int Take { get; set; } = 10;


        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int MinPrice { get; set; } = 0;


        [Range(SolutionConfiguration.MinPrice, SolutionConfiguration.MaxPrice)]
        public int MaxPrice { get; set; } = 20000;
    }
}

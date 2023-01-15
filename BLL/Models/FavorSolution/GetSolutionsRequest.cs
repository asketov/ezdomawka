using System.ComponentModel.DataAnnotations;

namespace BLL.Models.FavorSolution
{
    public class GetSolutionsRequest
    {
        public Guid? ThemeId { get; set; }
        public Guid? SubjectId { get; set; }
        [Required]
        [Range(0, 200000)]
        public int Skip { get; set; }
        [Required]
        [Range(10, 10)]
        public int Take { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

    }
}

using BLL.Models.Admin;
using DAL.Entities;

namespace BLL.Models.FavorSolution
{
    public class SolutionModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public string Price { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public User Author { get; set; } = null!;
        public DateTimeOffset Created { get; set; }
        public IEnumerable<SubjectModel> Subjects { get; set; } = null!;
        public ThemeModel Theme { get; set; } = null!;
    }
}

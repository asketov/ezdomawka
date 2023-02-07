

namespace BLL.Models.ViewModels
{
    public class EditSolutionVm : AddSolutionVm
    {
        public Guid Id { get; set; }
        public List<SubjectVm> SelectedSubjects { get; set; } = null!;
    }
}



using System.ComponentModel.DataAnnotations;

namespace BLL.Models.ViewModels
{
    public class EditSolutionVm : AddSolutionVm
    {
        [Required]
        public Guid Id { get; set; }
        
        
        [Required]
        public List<SubjectVm> SelectedSubjects { get; set; } = null!;
    }
}

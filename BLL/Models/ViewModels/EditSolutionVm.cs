using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class EditSolutionVm : AddSolutionVm
    {
        public Guid Id { get; set; }
        public IEnumerable<SubjectVm> SelectedSubjects { get; set; } = null!;
    }
}

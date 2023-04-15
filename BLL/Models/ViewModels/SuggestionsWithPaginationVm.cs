using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class SuggestionsWithPaginationVm
    {
        public IEnumerable<BLL.Models.ViewModels.SuggestionVm> Suggestions { get; set; } = null!;
        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.FavorSolution
{
    public class GetSolutionsModel
    {
        public Guid? ThemeId { get; set; }
        public Guid? SubjectId { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}

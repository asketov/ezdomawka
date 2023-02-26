using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Admin
{
    public class SubjectModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
       
    }
}

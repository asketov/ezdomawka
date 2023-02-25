using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Admin
{
    public class SubjectModel : IComparable<SubjectModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int CompareTo(SubjectModel? o)
        {
            if (o is null) throw new ArgumentException("Некорректное значение параметра");
            return Name.CompareTo(o.Name);
        }
    }
}

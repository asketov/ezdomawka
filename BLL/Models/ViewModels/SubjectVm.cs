using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class SubjectVm : IComparable<SubjectVm>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int CompareTo(SubjectVm? o)
        {
            if (o is null) throw new ArgumentException("Некорректное значение параметра");
            return Name.CompareTo(o.Name);
        }
    }
}

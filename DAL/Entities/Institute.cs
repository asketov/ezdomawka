using ModelsConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Institute
    {
        public Guid Id { get; set; }
        [MaxLength(InstituteConfiguration.MaxNameLength)]
        [MinLength(InstituteConfiguration.MinNameLength)]
        public string Name { get; set; } = null!;
        public ICollection<User> Students { get; set; } = null!;
        public ICollection<FavorSolution>? FavorSolutions { get; set; } = null!;
        public ICollection<Theme> Themes { get; set; } = null!;
        public ICollection<Subject> Subjects { get; set; } = null!;
        public ICollection<InstituteMailDomain>? InstituteMailDomains { get; set; } = null!;
    }
}

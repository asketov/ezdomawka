using ModelsConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class InstituteMailDomain
    {
        public Guid Id { get; set; }
        [MaxLength(InstituteMailDomainConfiguration.MaxNameLength)]
        [MinLength(InstituteMailDomainConfiguration.MinNameLength)]
        public string Name { get; set; } = null!;
        public Guid InstituteId { get; set; }
        public virtual Institute Institute { get; set; } = null!;
    }
}

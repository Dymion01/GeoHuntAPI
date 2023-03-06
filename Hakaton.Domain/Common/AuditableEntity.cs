using Hakaton.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Common
{
    public class AuditableEntity
    { 
        public int Id { get; set; }
        public int CreatedById { get; set; }
        [NotMapped]
        public virtual User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int StatusId { get; set; }
        public int InactivatedById { get; set; }
        public DateTime? InactivatedDate { get; set; }
    }
}

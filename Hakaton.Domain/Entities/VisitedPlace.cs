using Hakaton.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Entities
{
    public class VisitedPlace: AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoSrc { get; set; }
        public string Coordinates { get; set; }
        public int? ParentId { get; set; }
        public virtual VisitedPlace Parent { get; set; }
        public virtual ICollection<UserVisitedPlace> UserVisitedPlaces { get; set; }
    }
}

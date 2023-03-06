using Hakaton.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Entities
{
    public class UserVisitedPlace : AuditableEntity
    {

        public int UserId { get; set; }

        public int VisitedPlaceId { get; set; }

        public virtual User User {get; set;}
        public virtual VisitedPlace VisitedPlace { get; set; }
        
    }
}

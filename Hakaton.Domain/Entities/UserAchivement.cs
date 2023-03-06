using Hakaton.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Entities
{
    public class UserAchivement : AuditableEntity
    {
        public int UserId { get; set; }
        public int AchivementId { get; set; }

        public virtual User user { get; set;}
        public virtual Achivement Achievement { get; set; }


    }
}

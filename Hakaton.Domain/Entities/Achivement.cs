using Hakaton.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Entities
{
    public class Achivement : AuditableEntity
    {
        public string Name { get; set; }
        public string PhotoSrc { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserAchivement> UserAchivements { get; set; }
    }
}

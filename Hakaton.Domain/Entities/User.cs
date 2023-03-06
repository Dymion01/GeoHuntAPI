using Hakaton.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public IEnumerable<AppUserRole> UserRoles { get; set; }
        public string Biogram { get; set; }
        public int ScorePoints { get; set; }
        public virtual ICollection<UserVisitedPlace> UserVisitedPlaces { get; set; }
        public virtual ICollection<UserAchivement> UserAchivements { get; set; }
    }
}

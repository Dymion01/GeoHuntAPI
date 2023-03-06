using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Domain.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public AppRole Role { get; set; }
    }
}

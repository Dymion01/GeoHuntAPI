using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; set; }
        string Email { get; set; }
        bool IsAuthenticated { get; set; }
    }
}

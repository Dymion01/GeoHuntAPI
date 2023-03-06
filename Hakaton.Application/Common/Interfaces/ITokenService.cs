using Hakaton.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
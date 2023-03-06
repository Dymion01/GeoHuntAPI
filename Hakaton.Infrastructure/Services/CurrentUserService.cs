using Hakaton.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public int UserId { get; set; }
        public string Email {get; set;}
        public bool IsAuthenticated { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue("userId");
        }
    }
}

using Hakaton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hakaton.Application.Common.Interfaces
{
    public interface IAppDbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<VisitedPlace> VisitedPlaces { get; set; }
        DbSet<Achivement> Achievements { get; set; }
        DbSet<UserVisitedPlace> UserVisitedPlaces { get; set; }
        DbSet<UserAchivement> UserAchievements { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

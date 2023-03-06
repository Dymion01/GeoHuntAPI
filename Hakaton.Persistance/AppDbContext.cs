using Hakaton.Application.Common.Interfaces;
using Hakaton.Domain.Common;
using Hakaton.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hakaton.Persistance
{
    public class AppDbContext : IdentityDbContext<User, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>> ,IAppDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public AppDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }


        public DbSet<User> Users { get; set; }
        public DbSet<VisitedPlace> VisitedPlaces { get; set; }
        public DbSet<Achivement> Achievements{ get; set; }
        public DbSet<UserVisitedPlace> UserVisitedPlaces { get; set; }
        public DbSet<UserAchivement> UserAchievements { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();


            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

           


        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = _currentUserService.UserId;
                        //entry.Entity.CreatedById = 1;
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.StatusId = 1;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedById = _currentUserService.UserId;
                        entry.Entity.ModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.ModifiedById = _currentUserService.UserId;
                        entry.Entity.ModifiedDate = DateTime.Now;
                        entry.Entity.InactivatedDate = DateTime.Now;
                        entry.Entity.InactivatedById = _currentUserService.UserId;
                        entry.Entity.StatusId = 0;
                        entry.State = EntityState.Modified;
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

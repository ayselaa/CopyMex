using Mechanic.Data.Entity;
using Mechanic.Data.Entity.Contracts;
using Mechanic.Data.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data.Data
{
    //public class AppDbContext: IdentityDbContext<AppUser>
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, AppUserRole, IdentityUserLogin<string>,
             IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<AppUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<UserData> UserFINs { get; set; }
        public DbSet<Contract> Contracts { get; set; }



    }
}

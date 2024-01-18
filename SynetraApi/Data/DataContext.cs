using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SynetraApi.Models;

namespace SynetraApi.Data
{
    public class DataContext : IdentityDbContext<User,IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
           
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "User");
                entity.Ignore(c => c.PhoneNumber);
                entity.Ignore(c => c.PhoneNumberConfirmed);
        
              
            });
            builder.Entity<IdentityRole<int>>(entity => entity.ToTable(name: "Role"));
            builder.Entity<IdentityUserRole<int>>(entity => {
                entity.HasKey(r => new { r.UserId, r.RoleId });
                entity.ToTable(name: "UserRoles");
            });
            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(l => new { l.LoginProvider, l.ProviderKey });
                entity.ToTable(name: "UserLogins");
            });
            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });
            builder.Entity<IdentityRoleClaim<int>>(entity => {
                entity.ToTable(name: "RoleClaims");
               
            });
            builder.Entity<IdentityUserToken<int>>(entity => {
                entity.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
                entity.ToTable(name: "UserTokens");
             
            });
        }
       
        public DbSet<SynetraApi.Models.Room> Room { get; set; } = default!;
      
        public DbSet<SynetraApi.Models.Computer> Computer { get; set; } = default!;
        public DbSet<SynetraApi.Models.Parc> Parc { get; set; } = default!;
        public DbSet<SynetraApi.Models.Log> Log { get; set; } = default!;
    }
}

using EmailService_SiriusSoftware.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmailService_SiriusSoftware.DbContextConfig
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmailModel> Email { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is EmailModel entity && entry.State == EntityState.Added)
                {
                    entity.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone);
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
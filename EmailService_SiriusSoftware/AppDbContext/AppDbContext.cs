using EmailService_SiriusSoftware.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService_SiriusSoftware.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmailModel> Email { get; set; }
    }
}
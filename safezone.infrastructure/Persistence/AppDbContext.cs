using Microsoft.EntityFrameworkCore;
using safezone.domain.Entities;

namespace safezone.infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public DbSet<User> Users => Set<User>();
        public DbSet<Occurrence> Occurrences => Set<Occurrence>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occurrence>()
                .HasOne(o => o.User)
                .WithMany(u => u.Occurrences)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

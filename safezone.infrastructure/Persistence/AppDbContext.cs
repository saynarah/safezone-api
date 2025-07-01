using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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
                .OnDelete(DeleteBehavior.Cascade); // ou Restrict, conforme seu caso
        }

    }
}

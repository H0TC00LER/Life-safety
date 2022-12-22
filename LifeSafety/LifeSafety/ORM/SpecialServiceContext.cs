using LifeSafety.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeSafety.ORM
{
    public class SpecialServiceContext : DbContext
    {
        public DbSet<SpecialService> SpecialService { get; set; }

        public SpecialServiceContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LifeSafetyDB;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpecialService>().ToTable("ServiceNumbers");
            modelBuilder.Entity<SpecialService>().HasKey(p => p.NumberId);
            modelBuilder.Entity<SpecialService>().Property(p => p.ServiceName).IsRequired();
            modelBuilder.Entity<SpecialService>().Property(p => p.Number).IsRequired();
            modelBuilder.Entity<SpecialService>().Property(p => p.City).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

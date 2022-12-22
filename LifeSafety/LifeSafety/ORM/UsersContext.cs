using LifeSafety.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeSafety.ORM
{
    public class UsersContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public UsersContext()
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
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Users>().HasKey(p => p.userId);
            modelBuilder.Entity<Users>().Property(p => p.login).IsRequired();
            modelBuilder.Entity<Users>().Property(p => p.password).IsRequired();
            modelBuilder.Entity<Users>().Property(p => p.description).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

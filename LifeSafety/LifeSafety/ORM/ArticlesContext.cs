using LifeSafety.Models;
using Microsoft.EntityFrameworkCore;

namespace HttpServer
{
    public class ArticlesContext : DbContext
    {
        public DbSet<Articles> Articles { get; set; }

        public ArticlesContext()
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
            modelBuilder.Entity<Articles>().ToTable("Articles");
            modelBuilder.Entity<Articles>().HasKey(p => p.ArticleId);
            modelBuilder.Entity<Articles>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<Articles>().Property(p => p.Date).IsRequired();
            modelBuilder.Entity<Articles>().Property(p => p.Text).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

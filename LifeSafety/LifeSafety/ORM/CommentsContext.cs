using LifeSafety.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSafety.ORM
{
    public class CommentsContext : DbContext
    {
        public DbSet<Comments> Comments { get; set; }

        public CommentsContext()
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
            modelBuilder.Entity<Comments>().ToTable("Comments");
            modelBuilder.Entity<Comments>().Property(p => p.ArticleId).IsRequired();
            modelBuilder.Entity<Comments>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<Comments>().Property(p => p.Date).IsRequired();
            modelBuilder.Entity<Comments>().Property(p => p.Text).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}

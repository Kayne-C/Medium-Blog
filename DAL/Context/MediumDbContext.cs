using ENTITIES.Entity.Concrete;
using MAP.EntityTypeConfigration.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class MediumDbContext : DbContext
    {
        public MediumDbContext(DbContextOptions<MediumDbContext> options) : base(options)
        {

        }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new UserMap());

            modelBuilder.Entity<Article>().HasOne(a => a.Author).WithMany(u => u.Articles).HasForeignKey(u => u.AuthorId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }


       
        
    }
}

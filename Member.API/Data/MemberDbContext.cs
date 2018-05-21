using Member.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Member.API.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User").HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(u => u.Phone).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>().Property(u => u.Name).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Company).HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Position).HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavorSubject>()
                .HasKey(bc => new { bc.FavorSolutionId, bc.SubjectId });
            modelBuilder.Entity<FavorSubject>()
                .HasOne(bc => bc.FavorSolution)
                .WithMany(b => b.FavorSubjects)
                .HasForeignKey(bc => bc.FavorSolutionId);
            modelBuilder.Entity<FavorSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.FavorSubjects)
                .HasForeignKey(bc => bc.SubjectId);
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<FavorSolution> FavorSolutions => Set<FavorSolution>();
        public DbSet<Theme> Themes => Set<Theme>();
        public DbSet<Role> Roles => Set<Role>();

    }
}

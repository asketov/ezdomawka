using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<FavorSolution> FavorSolutions => Set<FavorSolution>();
        public DbSet<Theme> Themes => Set<Theme>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Suggestion> Suggestions => Set<Suggestion>();
        public DbSet<Ban> Bans => Set<Ban>();
        public DbSet<Report> Reports => Set<Report>();
        public DbSet<FavorSubject> FavorSubject => Set<FavorSubject>();
        public DbSet<UpdateFavorHistory> UpdateFavorHistory => Set<UpdateFavorHistory>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<FavoriteFavor> FavoriteFavors => Set<FavoriteFavor>();
        
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubjectDataContextConfiguration());
            modelBuilder.ApplyConfiguration(new FavorSubjectDataContextConfiguration());
            modelBuilder.ApplyConfiguration(new ThemeDataContextConfiguration());

            modelBuilder.Entity<FavorSolution>(b =>
            {
                b.HasOne(ff => ff.Author)
                    .WithMany(ff => ff.FavorSolutions)
                    .HasForeignKey(ff => ff.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                b.HasMany(ff => ff.FavorSubjects)
                    .WithOne(ff => ff.FavorSolution)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }
}

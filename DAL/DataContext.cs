using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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
                .HasForeignKey(bc => bc.FavorSolutionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FavorSubject>()
                .HasOne(bc => bc.Subject)
                .WithMany(c => c.FavorSubjects)
                .HasForeignKey(bc => bc.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Theme>()
                .HasMany(x => x.FavorSolutions)
                .WithOne(x => x.Theme)
                .HasForeignKey(x => x.ThemeId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Subject>()
                .HasMany(x => x.FavorSubjects)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity("DAL.Entities.FavorSolution", b =>
            {
                b.HasOne("DAL.Entities.User", "Author")
                    .WithMany("FavorSolutions")
                    .HasForeignKey("AuthorId")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                b.HasMany("DAL.Entities.FavorSubject", "FavorSubjects")
                    .WithOne("FavorSolution")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
            modelBuilder.Entity<FavoriteFavor>().ToTable("FavoriteFavor");
        }
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
    }
}

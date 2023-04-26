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
        public DbSet<FavorSubject> FavorSubjects => Set<FavorSubject>();
        public DbSet<UpdateFavorHistory> UpdateFavorHistory => Set<UpdateFavorHistory>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<FavoriteFavor> FavoriteFavors => Set<FavoriteFavor>();
        public DbSet<Institute> Institutes => Set<Institute>();
        public DbSet<InstituteMailDomain> MailDomainInstitutes => Set<InstituteMailDomain>();

        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FavorSubject>().HasKey(bc => new { bc.FavorSolutionId, bc.SubjectId });
            modelBuilder.Entity<FavorSubject>().HasOne(bc => bc.FavorSolution)
                    .WithMany(b => b.FavorSubjects)
                    .HasForeignKey(bc => bc.FavorSolutionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<FavorSubject>().HasOne(bc => bc.Subject)
                    .WithMany(c => c.FavorSubjects)
                    .HasForeignKey(bc => bc.SubjectId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<Subject>().HasOne(bc => bc.Institute)
                    .WithMany(c => c.Subjects)
                    .HasForeignKey(bc => bc.InstituteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<Theme>().HasOne(bc => bc.Institute)
                    .WithMany(c => c.Themes)
                    .HasForeignKey(bc => bc.InstituteId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<FavorSolution>().HasOne(ff => ff.Author)
                    .WithMany(ff => ff.FavorSolutions)
                    .HasForeignKey(ff => ff.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<FavorSolution>().HasOne(bc => bc.Institute)
                    .WithMany(c => c.FavorSolutions)
                    .HasForeignKey(bc => bc.InstituteId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            modelBuilder.Entity<FavorSolution>().HasOne(ff => ff.Theme)
                    .WithMany(ff => ff.FavorSolutions)
                    .HasForeignKey(bc => bc.ThemeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<InstituteMailDomain>().HasOne(ff => ff.Institute)
                    .WithMany(ff => ff.InstituteMailDomains)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            modelBuilder.Entity<Report>().HasOne(ff => ff.FavorSolution)
                   .WithMany(ff => ff.Reports)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}

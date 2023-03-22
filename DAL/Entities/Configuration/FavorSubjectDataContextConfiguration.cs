using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration;

public class FavorSubjectDataContextConfiguration : IEntityTypeConfiguration<FavorSubject>
{
    public void Configure(EntityTypeBuilder<FavorSubject> builder)
    {
        builder.HasKey(bc => new { bc.FavorSolutionId, bc.SubjectId });
        
        builder.HasOne(bc => bc.FavorSolution)
            .WithMany(b => b.FavorSubjects)
            .HasForeignKey(bc => bc.FavorSolutionId)
            .OnDelete(DeleteBehavior.Restrict);
        
       builder.HasOne(bc => bc.Subject)
            .WithMany(c => c.FavorSubjects)
            .HasForeignKey(bc => bc.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
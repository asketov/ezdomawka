using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration;

public class ThemeDataContextConfiguration : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> builder)
    {
       builder.HasMany(x => x.FavorSolutions)
            .WithOne(x => x.Theme)
            .HasForeignKey(x => x.ThemeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Extensions;

public static class DbConfigurationExtensions
{
    public static EntityTypeBuilder<TEntity> HasMinLength<TEntity>
        (this EntityTypeBuilder<TEntity> builder, string propName, int minLength)
        where TEntity : class
    {
        builder.HasCheckConstraint( propName, $"DATALENGTH({propName}) >= {minLength}");
        
        return builder;
    }
}
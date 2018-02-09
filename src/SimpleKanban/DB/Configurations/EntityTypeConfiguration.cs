using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleKanban.DB.Configurations
{
    abstract class EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        internal abstract void Map(EntityTypeBuilder<TEntity> builder);
    }

    static class ModelBuilderExtensions
    {
        internal static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration)
            where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}

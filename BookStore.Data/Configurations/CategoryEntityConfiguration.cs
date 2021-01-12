using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Configurations
{
    internal sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.OwnsOne(cat => cat.Name, cfg =>
            {
                cfg.Property(x => x.Value).HasColumnName("Name").HasMaxLength(100);
            });

        }
    }
}

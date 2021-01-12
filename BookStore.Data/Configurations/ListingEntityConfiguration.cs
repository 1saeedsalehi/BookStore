using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Configurations
{
    internal sealed class ListingEntityConfiguration : IEntityTypeConfiguration<ListingEntity>
    {
        public void Configure(EntityTypeBuilder<ListingEntity> builder)
        {
            builder.OwnsOne(book => book.Title, cfg =>
            {

                cfg.Property(x => x.Value).HasColumnName("Title").HasMaxLength(100);
            });
        }
    }
}

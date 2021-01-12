using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Data.Configurations
{
    internal sealed class BookEntityConfiguration : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.OwnsOne(book => book.Name, cfg =>
            {

                cfg.Property(x => x.Value).HasColumnName("Name").HasMaxLength(100);
            });

            //TODO: issue with owned property seed method!!!
            //builder.HasData(new BookEntity
            //{
            //    Id = 1,
            //    AuthorName = "رولف دوبلی",
            //    Name = new NotEmptyString(),
            //    CoverImage = "https://cdn.fidibo.com/images/books/100126_77640_normal.jpg",
            //    Price = Money.From(20000),
            //    Title = NotEmptyString.From("کتاب صوتی هنر شفاف اندیشیدن | اثر رولف دوبلی | با صدای عادل فردوسی پور"),
            //    Publisher = "فیدیبو"
            //});

            builder.OwnsOne(book => book.Title, cfg =>
            {
                cfg.Property(x => x.Value).HasColumnName("Title").HasMaxLength(200);
            });

            builder.OwnsOne(book => book.Price, cfg =>
            {
                cfg.Property(x => x.Value).HasColumnName("Price");
            });
            builder.Property(x => x.AuthorName).HasMaxLength(100);
            builder.Property(x => x.Translator).HasMaxLength(100);
            builder.Property(x => x.Publisher).HasMaxLength(100);
            builder.Property(x => x.CoverImage).HasMaxLength(1000);
            builder.Property(x => x.Descripion).HasMaxLength(4000);
            builder.Property(x => x.Keywords).HasMaxLength(500);


        }
    }
}

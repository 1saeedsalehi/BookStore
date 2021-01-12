using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<ListingEntity> Listings { get; set; }

        public DbSet<ListingBook> ListingBooks { get; set; }

    }
}

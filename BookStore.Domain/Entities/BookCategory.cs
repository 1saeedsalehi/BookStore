using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class BookCategory : BaseEntity
    {
        [ForeignKey(nameof(BookId))]
        public BookEntity Book { get; set; }
        public int BookId { get; set; }


        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Category { get; set; }
        public int CategoryId { get; set; }
    }
}

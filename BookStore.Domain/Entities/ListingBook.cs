using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Domain.Entities
{
    public class ListingBook : BaseEntity
    {
        [ForeignKey(nameof(ListingId))]
        public ListingEntity Listing { get; set; }
        public int ListingId { get; set; }

        [ForeignKey(nameof(BookId))]
        public BookEntity Book { get; set; }
        public int BookId { get; set; }
    }
}

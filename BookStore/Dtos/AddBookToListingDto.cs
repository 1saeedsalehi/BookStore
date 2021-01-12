using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos
{
    public class AddBookToListingDto
    {
        public int ListingId { get; set; }
        public List<int> BookIds { get; set; }
    }
}

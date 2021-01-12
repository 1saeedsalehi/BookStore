using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos
{
    public class BookListingDto
    {
        public string ListingTitle { get; set; }
        public List<BookDto> Books { get; set; }
    }
}

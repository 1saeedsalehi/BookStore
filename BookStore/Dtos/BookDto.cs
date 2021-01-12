using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Dtos
{
    public class BookDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Translator { get; set; }
        public int PageCount { get; set; }
        public string Publisher { get; set; }
        public DateTimeOffset PublishedOn { get; set; }

        public string CoverImage { get; set; }
        public string Descripion { get; set; }
        public decimal Price { get; set; }
    }
}

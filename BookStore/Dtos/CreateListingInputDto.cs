using System.Collections.Generic;

namespace BookStore.Dtos
{
    public class CreateListingInputDto
    {
        public string Title { get; set; }
        public int DisplayOrder { get; set; }
        public int PageSize { get; set; }

    }
}

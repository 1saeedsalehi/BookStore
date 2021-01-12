using System.Collections.Generic;

namespace BookStore.Dtos
{
    public class PaginationInputDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public PaginationInputDto()
        {
            Page = 1;
            PageSize = 10;
        }

    }
}

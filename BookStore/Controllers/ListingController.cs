using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Data;
using BookStore.Domain.Entities;
using BookStore.Dtos;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("~/[controller]")]
    public class ListingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BookStoreDbContext _dbContext;

        public ListingController(IMapper mapper, BookStoreDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public class BookListingDto
        {
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(ResultModel<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SearchBookListing(string searchQuery, CancellationToken cancellationToken)
        {


            var result = _dbContext.ListingBooks
                .AsNoTracking()
                 .Include(x => x.Book)
                 .Include(x => x.Listing)
                 .Where(x => x.Listing.Title.Value.Contains(searchQuery))
                 .AsEnumerable()
                 .GroupBy(x => x.Listing.Title);
                 


            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllBookListing([FromQuery] PaginationInputDto input, CancellationToken cancellationToken)
        {


            var result =  _dbContext.ListingBooks
                .AsNoTracking()
                 .Include(x => x.Book)
                 .Include(x => x.Listing)
                 .AsEnumerable()
                 .GroupBy(x => x.Listing.Title)
                 .Skip(input.Page - 1)
                 .Take(input.Page * input.PageSize);
                 

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResultModel<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateBookListing(CreateListingInputDto input, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ListingEntity>(input);

            _dbContext.Add(mapped);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok(mapped.Id.ToResultModel());
        }

        [HttpPost("add-book")]
        [ProducesResponseType(typeof(ResultModel<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AddBookToListing(AddBookToListingDto input, CancellationToken cancellationToken)
        {

            var listing = await _dbContext.Listings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == input.ListingId, cancellationToken);

            foreach (var id in input.BookIds)
            {
                _dbContext.ListingBooks.Add(new ListingBook { BookId = id, ListingId = listing.Id });
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok(listing.Id);
        }
        [HttpDelete]
        [ProducesResponseType(typeof(ResultModel<int>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteListing(int listingId, CancellationToken cancellationToken)
        {


            var itemToDelete = await _dbContext.Listings
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == listingId, cancellationToken);

            _dbContext.Listings.Remove(itemToDelete);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Ok(itemToDelete.Id);
        }
    }
}

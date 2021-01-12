using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Data;
using BookStore.Domain.Entities;
using BookStore.Dtos;
using BookStore.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                 .Where(x => x.Listing.Title.Val.Contains(searchQuery))
                 .AsEnumerable()
                 .GroupBy(x => x.Listing.Title);



            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResultModel<List<BookListingDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllBookListing([FromQuery] PaginationInputDto input, CancellationToken cancellationToken)
        {

            var result = new List<BookListingDto>();
            var groupped = _dbContext.ListingBooks
                .AsNoTracking()
                 .Include(x => x.Book)
                 .Include(x => x.Listing)
                 .OrderByDescending(x => x.Listing.DisplayOrder)
                 .AsEnumerable()
                 .GroupBy(x => x.Listing.Title)
                 .Skip(input.Page - 1)
                 .Take(input.Page * input.PageSize);

            //TODO: fix this! jsut for client side test!
            foreach (var item in groupped)
            {
                var toAdd = new BookListingDto
                {
                    ListingTitle = item.Key.Val,
                    Books = new List<BookDto>()

                };
                foreach (var book in item)
                {
                    toAdd.Books.Add(new BookDto
                    {
                        AuthorName = book.Book.AuthorName,
                        CoverImage = book.Book.CoverImage,
                        Descripion = book.Book.Descripion,
                        Name = book.Book.Name.Val,
                        PageCount = book.Book.PageCount,
                        Price = book.Book.Price.Val,
                        PublishedOn = book.Book.PublishedOn,
                        Publisher = book.Book.Publisher,
                        Title = book.Book.Title.Val,
                        Translator = book.Book.Translator
                    });


                }
                result.Add(toAdd);
            }



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

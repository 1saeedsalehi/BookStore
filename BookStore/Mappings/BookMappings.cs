using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.Dtos;

namespace BookStore.Mappings
{
    internal sealed class BookMappings : Profile
    {
        public BookMappings()
        {
            CreateMap<BookInputDto, BookEntity>();
        }
    }
}

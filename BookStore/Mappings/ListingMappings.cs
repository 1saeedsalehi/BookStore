using AutoMapper;
using BookStore.Domain.Entities;
using BookStore.Domain.ValueObjects;
using BookStore.Dtos;

namespace BookStore.Mappings
{
    internal sealed class ListingMappings : Profile
    {
        public ListingMappings()
        {
            CreateMap<CreateListingInputDto, ListingEntity>()
            .ForMember(request => request.Title, opt =>
                opt.MapFrom(input => NotEmptyString.From(input.Title)));

        }
    }
}

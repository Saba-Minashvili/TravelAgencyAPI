using AutoMapper;
using Contracts;
using Domain.Entities;

namespace Persistence.Mapper
{
    public class ObjectMapper : Profile
    {
        public ObjectMapper()
        {
            CreateMap<User, UserDto>()
                .ForMember(o => o.Apartment, k => k.MapFrom(m => m.Apartment))
                .ReverseMap();
            CreateMap<User, RegisterUserDto>()
                .ReverseMap();
            CreateMap<User, UpdateUserDto>()
                .ReverseMap();
            CreateMap<Apartment, ApartmentDto>()
                .ReverseMap();
            CreateMap<Guest, GuestDto>()
                .ReverseMap();
            CreateMap<Book, BookDto>()
                .ReverseMap();
            CreateMap<BookRequest, BookRequestDto>()
                .ReverseMap();
        }
    }
}

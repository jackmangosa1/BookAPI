using AutoMapper;
using BookAPI.DTO;
using BookAPI.Models;

namespace BookAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using GameLibrary.Domain.Dto;
using GameLibrary.Domain.Entities;

namespace GameLibrary.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Genre, GenreDto>()
                .ReverseMap();
            CreateMap<Game, GameDto>()
                .ReverseMap();
        }
    }
}

using AutoMapper;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.Position;
using SmartMarket.Service.DTOs.Workers;

namespace SmartMarket.Service.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Category, CategoryDto>();

        /*-------------Worker----------*/

        CreateMap<AddWrokerDto, Worker>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()) 
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<Worker, WorkerDto>();

        /*---------Position-----------*/

        CreateMap<AddPositionDto, Position>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Position, PositionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));


    }
}

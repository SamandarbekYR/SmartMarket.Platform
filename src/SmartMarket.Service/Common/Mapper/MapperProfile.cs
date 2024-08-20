using AutoMapper;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Service.DTOs.Category;

namespace SmartMarket.Service.Common.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Category, CategoryDto>();
    }
}

using AutoMapper;
using Sample.Api.Api.Models.Request;
using Sample.Api.Api.Models.Response;
using Sample.Api.Domain.Categories;

namespace Sample.Api.Api.Mappings;

public class CategoriesApiMappingProfile : Profile
{
    public CategoriesApiMappingProfile()
    {
        CreateMap<Category, CategoryResponseItem>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();
    }
}

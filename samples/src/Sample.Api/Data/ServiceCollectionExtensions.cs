using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Data.Categories;
using Sample.Api.Data.Products;
using Sample.Api.Domain.Categories;
using Sample.Api.Domain.Products;

namespace Sample.Api.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICategoriesRepository, CategoriesRepository>()
            .AddScoped<IProductsRepository, ProductsRepository>();
    }
}

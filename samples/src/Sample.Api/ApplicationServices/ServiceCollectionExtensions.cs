using Microsoft.Extensions.DependencyInjection;

namespace Sample.Api.ApplicationServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.AddScoped<CategoriesService>();
    }
}

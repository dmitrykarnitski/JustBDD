using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Api.Configuration;
using Sample.Api.Api.Models.Request;
using Sample.Api.Api.RequestPipeline.Authentication;
using Sample.Api.Api.RequestPipeline.Authorization;
using Sample.Api.Api.RequestPipeline.Swagger;
using Sample.Api.Api.Validators;
using Sample.Api.ApplicationServices;
using Sample.Api.Data;
using Sample.Api.Framework.Configuration;

namespace Sample.Api.Api.IoC;

public static class ServiceCollectionExtensions
{
    private const string ApiVersion = "v1";

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services
            .AddSharedServices()
            .AddDataServices()
            .AddApplicationServices()
            .AddApiServices();
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCategoryRequest>, CreateCategoryRequestValidator>();
        services.AddScoped<IValidator<UpdateCategoryRequest>, UpdateCategoryRequestValidator>();

        services.AddControllers();

        return services
            .AddAuthenticationServices()
            .AddAuthorizationServices()
            .AddSwaggerDocs(ApiVersion);
    }

    private static IServiceCollection AddSharedServices(this IServiceCollection services)
    {
        return services
            .AddConfiguration<ApplicationConfiguration>()
            .AddAutoMapper(typeof(ServiceCollectionExtensions));
    }
}

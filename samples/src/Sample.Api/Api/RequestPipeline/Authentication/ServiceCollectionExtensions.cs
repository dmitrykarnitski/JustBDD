using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Api.Configuration;
using Sample.Api.Framework.Configuration;

namespace Sample.Api.Api.RequestPipeline.Authentication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddConfiguration<AuthenticationConfiguration>();

        services.AddSingleton<IJwtBearerOptionsConfigurationApplier, JwtBearerOptionsConfigurationApplier>();

        services.AddOptions<JwtBearerOptions>(AuthenticationConstants.Scheme)
            .Configure<IJwtBearerOptionsConfigurationApplier, AuthenticationConfiguration>(
                (options, configurationApplier, authConfig) => configurationApplier.Apply(options, authConfig));

        services.AddAuthentication(AuthenticationConstants.Scheme)
            .AddJwtBearer(AuthenticationConstants.Scheme, _ => { });

        return services;
    }
}

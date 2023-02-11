using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sample.Api.Api.Configuration;
using Sample.Api.Api.RequestPipeline.Authentication;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sample.Api.Api.RequestPipeline.Swagger;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services, string version)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        // TODO: use configure for everything
        services.AddOptions<SwaggerGenOptions>()
            .Configure<AuthenticationConfiguration, ApplicationConfiguration>(
            (options, authConfig, appConfig) =>
            {
                var swaggerInfo = new OpenApiInfo
                {
                    Title = appConfig.Name,
                    Version = version
                };

                options.CustomSchemaIds(type => type.FullName);
                options.SwaggerDoc(version, swaggerInfo);
                options.AddSecurityDefinition(
                    AuthenticationConstants.Scheme,
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OpenIdConnect,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(authConfig.AuthorizationUrl, UriKind.Absolute)
                            }
                        }
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = AuthenticationConstants.Scheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        services
            .AddOptions<SwaggerUIOptions>()
            .Configure<AuthenticationConfiguration, ApplicationConfiguration>(
                (options, authConfig, appConfig) =>
                {
                    options.OAuthClientId(authConfig.ClientId);
                    options.OAuthClientSecret(authConfig.ClientSecret);
                    options.OAuthRealm(authConfig.ClientId);
                    options.OAuthAppName(appConfig.Name);
                    options.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "resource", authConfig.Resource } });
                    options.SwaggerEndpoint($"./{version}/swagger.json", appConfig.Name);
                });

        return services;
    }
}

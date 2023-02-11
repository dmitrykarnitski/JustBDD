using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Api.Framework.Configuration;

public static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration<TConfiguration>(this IServiceCollection services) where TConfiguration : class, new()
    {
        services.AddSingleton(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var configurationObject = new TConfiguration();

            var bindingKey = GetSectionName<TConfiguration>();

            configuration.Bind(bindingKey, configurationObject);

            Validator.ValidateObject(configurationObject, new ValidationContext(configurationObject), true);

            return configurationObject;
        });

        return services;
    }

    private static string GetSectionName<TConfiguration>()
    {
        return typeof(TConfiguration).Name
            .Replace("configuration", string.Empty, StringComparison.OrdinalIgnoreCase)
            .Replace("options", string.Empty, StringComparison.OrdinalIgnoreCase);
    }
}

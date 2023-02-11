using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Sample.Api.Api.RequestPipeline.Authentication;

namespace Sample.Api.Api.RequestPipeline.Authorization;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        return services.AddAuthorization(o =>
        {
            o.DefaultPolicy = new AuthorizationPolicyBuilder(AuthenticationConstants.Scheme)
                .RequireAuthenticatedUser()
                .Build();
        });
    }
}

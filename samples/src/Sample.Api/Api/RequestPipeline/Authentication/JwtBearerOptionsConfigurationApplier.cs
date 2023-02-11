using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sample.Api.Api.Configuration;

namespace Sample.Api.Api.RequestPipeline.Authentication;

public class JwtBearerOptionsConfigurationApplier : IJwtBearerOptionsConfigurationApplier
{
    public virtual void Apply(JwtBearerOptions options, AuthenticationConfiguration authConfiguration)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RequireSignedTokens = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateTokenReplay = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ValidAudiences = new[] { authConfiguration.Resource }
        };

        options.Authority = authConfiguration.Issuer;
    }
}

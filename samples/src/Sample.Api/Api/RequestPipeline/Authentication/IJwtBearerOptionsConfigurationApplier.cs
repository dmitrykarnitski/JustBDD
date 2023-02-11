using Microsoft.AspNetCore.Authentication.JwtBearer;
using Sample.Api.Api.Configuration;

namespace Sample.Api.Api.RequestPipeline.Authentication;

public interface IJwtBearerOptionsConfigurationApplier
{
    void Apply(JwtBearerOptions options, AuthenticationConfiguration authConfiguration);
}

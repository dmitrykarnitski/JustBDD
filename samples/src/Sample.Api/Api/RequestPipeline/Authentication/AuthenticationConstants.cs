using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Sample.Api.Api.RequestPipeline.Authentication;

public static class AuthenticationConstants
{
    public const string Scheme = JwtBearerDefaults.AuthenticationScheme;
}

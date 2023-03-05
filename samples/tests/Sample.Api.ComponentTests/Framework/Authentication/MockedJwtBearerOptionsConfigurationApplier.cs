using Microsoft.AspNetCore.Authentication.JwtBearer;
using Sample.Api.Api.Configuration;
using Sample.Api.Api.RequestPipeline.Authentication;

namespace Sample.Api.ComponentTests.Framework.Authentication;

public class MockedJwtBearerOptionsConfigurationApplier : JwtBearerOptionsConfigurationApplier
{
    public override void Apply(JwtBearerOptions options, AuthenticationConfiguration authConfiguration)
    {
        base.Apply(options, authConfiguration);

        options.BackchannelHttpHandler = new MockBackChannelHttpHandler();
        options.MetadataAddress = MockAuthenticationConstants.OpenIdConfigurationUri;
    }
}

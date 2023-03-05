namespace Sample.Api.ComponentTests.Framework.Authentication;

public static class MockAuthenticationConstants
{
    public const string Issuer = "https://dummy.url";

    public const string OpenIdConfigurationUri = Issuer + "/.well-known/openid-configuration";

    public const string JwksUri = Issuer + "/jwks";

    public const string Resource = "api://dummy-api";

    public const string KeyId = "dummy-key-id";
}

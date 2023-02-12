using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Sample.Api.Tests.InMemory.Integration.Framework.Authentication.Resources;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Authentication;

public class MockJwtTokenGenerator
{
    public string Generate(ClaimsIdentity claimsIdentity, string? issuer, string? audience, DateTime expiresAt)
    {
        using var rsa = MockedAuthenticationResourcesReader.GetPrivateRsaKey();

        var rsaSecurityKey = new RsaSecurityKey(rsa)
        {
            KeyId = MockAuthenticationConstants.KeyId
        };

        var signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256Signature)
        {
            CryptoProviderFactory = new CryptoProviderFactory
            {
                CacheSignatureProviders = false
            }
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Expires = expiresAt,
            NotBefore = expiresAt.AddHours(-2),
            Subject = claimsIdentity,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}

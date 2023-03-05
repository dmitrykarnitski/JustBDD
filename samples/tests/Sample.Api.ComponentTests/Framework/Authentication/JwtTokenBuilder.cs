using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Sample.Api.ComponentTests.Framework.Authentication;

public class JwtTokenBuilder
{
    private readonly MockJwtTokenGenerator _jwtTokenGenerator = new();

    private readonly ICollection<Claim> _claims = new List<Claim>();
    private string? _issuer;
    private string? _audience;
    private DateTime _expiresAt;

    public JwtTokenBuilder WithValidMockedAuthConfiguration()
    {
        _issuer = MockAuthenticationConstants.Issuer;
        _audience = MockAuthenticationConstants.Resource;
        _expiresAt = DateTime.UtcNow.AddHours(2);

        return this;
    }

    public JwtTokenBuilder WithAnExpiresAtInThePast()
    {
        // making sure that token expiration time is as close to current time as possible
        // making sure that clock skew is also have taken into account for robustness
        _expiresAt = DateTime.UtcNow.Subtract(TokenValidationParameters.DefaultClockSkew).Subtract(TimeSpan.FromSeconds(1));

        return this;
    }

    public JwtTokenBuilder WithSubject(string subject)
    {
        _claims.Add(new Claim(JwtRegisteredClaimNames.Sub, subject));

        return this;
    }

    public string Build()
    {
        return _jwtTokenGenerator.Generate(new ClaimsIdentity(_claims), _issuer, _audience, _expiresAt);
    }
}

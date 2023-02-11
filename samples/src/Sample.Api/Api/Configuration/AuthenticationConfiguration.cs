using System.ComponentModel.DataAnnotations;

namespace Sample.Api.Api.Configuration;

public class AuthenticationConfiguration
{
    [Required]
    public string Issuer { get; set; } = null!;

    public string AuthorizationUrl => $"{Issuer.TrimEnd('/')}/oauth2/authorize";

    [Required]
    public string ClientId { get; set; } = null!;

    [Required]
    public string ClientSecret { get; set; } = null!;

    [Required]
    public string Resource { get; set; } = null!;
}

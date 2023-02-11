using System.ComponentModel.DataAnnotations;

namespace Sample.Api.Api.Configuration;

public class ApplicationConfiguration
{
    [Required]
    public string Name { get; init; } = null!;
}

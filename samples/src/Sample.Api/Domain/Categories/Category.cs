using System;

namespace Sample.Api.Domain.Categories;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; }
}

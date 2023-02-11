using Sample.Api.Domain.Categories;

namespace Sample.Api.Domain.Products;

public class Product
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string Description { get; init; } = null!;

    public Category Category { get; init; } = null!;

    public decimal Price { get; set; }
}

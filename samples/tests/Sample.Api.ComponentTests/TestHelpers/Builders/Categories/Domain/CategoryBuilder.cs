using AutoFixture;
using Sample.Api.ComponentTests.Framework.Extensions;
using Sample.Api.Domain.Categories;

namespace Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Domain;

public class CategoryBuilder
{
    private readonly Fixture _fixture = new();

    private Category _request = new();

    public CategoryBuilder WithRandomValues()
    {
        _request = _fixture.Create<Category>();

        return this;
    }

    public CategoryBuilder WithId(int id)
    {
        _request.Id = id;

        return this;
    }

    public CategoryBuilder WithName(string? name)
    {
        _request.Name = name!;

        return this;
    }

    public Category Build()
    {
        return _request.DeepClone();
    }
}

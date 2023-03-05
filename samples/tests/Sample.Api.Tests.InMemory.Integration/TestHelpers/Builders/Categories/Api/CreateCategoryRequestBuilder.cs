using AutoFixture;
using AutoFixture.Dsl;
using Sample.Api.Api.Models.Request;

namespace Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;

public class CreateCategoryRequestBuilder
{
    private readonly Fixture _fixture = new();

    private IPostprocessComposer<CreateCategoryRequest> _request = new Fixture().Build<CreateCategoryRequest>();

    public CreateCategoryRequestBuilder WithRandomValues()
    {
        _request = _fixture.Build<CreateCategoryRequest>();

        return this;
    }

    public CreateCategoryRequestBuilder WithName(string? name)
    {
        _request = _request.With(x => x.Name, name);

        return this;
    }

    public CreateCategoryRequest Build()
    {
        return _request.Create();
    }
}

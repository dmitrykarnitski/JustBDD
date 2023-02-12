using AutoFixture;
using Sample.Api.Api.Models.Request;
using Sample.Api.Tests.InMemory.Integration.Framework.Extensions;

namespace Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;

public class CreateCategoryRequestBuilder
{
    private readonly Fixture _fixture = new();

    private CreateCategoryRequest _request = new();

    public CreateCategoryRequestBuilder WithRandomValues()
    {
        _request = _fixture.Create<CreateCategoryRequest>();

        return this;
    }

    public CreateCategoryRequest Build()
    {
        return _request.DeepClone()!;
    }
}

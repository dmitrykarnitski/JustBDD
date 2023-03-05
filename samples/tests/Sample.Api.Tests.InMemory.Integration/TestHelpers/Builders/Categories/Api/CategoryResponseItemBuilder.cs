using Sample.Api.Api.Models.Response;
using Sample.Api.Domain.Categories;
using Sample.Api.Tests.InMemory.Integration.Framework.Extensions;

namespace Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;

public class CategoryResponseItemBuilder
{
    private CategoryResponseItem _response = new();

    public CategoryResponseItemBuilder FromDomainModel(Category category)
    {
        _response = new CategoryResponseItem
        {
            Id = category.Id,
            Name = category.Name
        };

        return this;
    }

    public CategoryResponseItem Build()
    {
        return _response.DeepClone();
    }
}

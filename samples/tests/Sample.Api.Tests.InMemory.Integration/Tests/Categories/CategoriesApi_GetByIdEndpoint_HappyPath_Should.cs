using NUnit.Framework;
using Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Domain;

namespace Sample.Api.Tests.InMemory.Integration.Tests.Categories;

public class CategoriesApi_GetByIdEndpoint_HappyPath_Should : TestFixtureBase
{
    [Test]
    public void ReturnExistingCategoryById()
    {
        var existingCategory = new CategoryBuilder()
            .WithRandomValues()
            .Build();

        var expectedResponse = new CategoryResponseItemBuilder()
            .FromDomainModel(existingCategory)
            .Build();

        Given.IHave.LoggedInAs.AValidUser().And.DatabaseHas.Category(existingCategory);
        When.ICall.TheCategoriesApi.GetByIdEndpoint(existingCategory.Id);
        Then.TheCall.WillSucceed().And.TheCall.WillHaveAResponseEqualTo(expectedResponse);
    }
}

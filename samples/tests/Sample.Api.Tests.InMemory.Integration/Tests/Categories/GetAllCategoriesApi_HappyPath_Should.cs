using NUnit.Framework;
using Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Domain;

namespace Sample.Api.Tests.InMemory.Integration.Tests.Categories;

public class GetAllCategoriesApiHappyPathShould : TestFixtureBase
{
    [Test]
    public void ReturnAllCategories()
    {
        var existingCategory = new CategoryBuilder()
            .WithRandomValues()
            .Build();

        var expectedResponse = new CategoryResponseItemBuilder()
            .FromDomainModel(existingCategory)
            .Build();

        Given.IHave.LoggedInAs.AValidUser().And.DatabaseHas.Category(existingCategory);
        When.ICall.TheCategoriesApi.GetAllEndpoint();
        Then.TheCall.WillSucceed().And.TheCall.WillHaveAResponseEqualTo(new[] { expectedResponse });
    }
}

using NUnit.Framework;
using Sample.Api.ComponentTests.Framework.FixtureSupport;
using Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Api;
using Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Domain;

namespace Sample.Api.ComponentTests.Tests.Categories;

[TestFixture]
public class CategoriesApi_GetAllEndpoint_HappyPath_Should : TestFixtureBase
{
    [Test]
    public void ReturnAllCategories()
    {
        var existingCategory = new CategoryBuilder()
            .WithRandomValues()
            .Build();

        var expectedResponseItem = new CategoryResponseItemBuilder()
            .FromDomainModel(existingCategory)
            .Build();

        Given.IHave.LoggedInAs.AValidUser().And.Database.HasCategory(existingCategory);
        When.ICall.TheCategoriesApi.GetAllEndpoint();
        Then.TheCall.WillSucceed().And.TheCall.WillHaveAResponse.CollectionEqualTo(new[] { expectedResponseItem });
    }
}

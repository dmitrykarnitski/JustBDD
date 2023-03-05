using System.Net;
using NUnit.Framework;
using Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Domain;

namespace Sample.Api.Tests.InMemory.Integration.Tests.Categories;

public class CategoriesApi_CreateEndpoint_HappyPath_Should : TestFixtureBase
{
    [Test]
    public void CreateANewCategory()
    {
        var existingCategory = new CategoryBuilder()
            .WithRandomValues()
            .Build();

        var request = new CreateCategoryRequestBuilder()
            .WithRandomValues()
            .Build();

        var expectedCategory = new CategoryBuilder()
            .WithId(existingCategory.Id + 1)
            .WithName(request.Name)
            .Build();

        Given.IHave.LoggedInAs.AValidUser().And.DatabaseHas.Category(existingCategory);
        When.ICall.TheCategoriesApi.CreateEndpoint(request);
        Then.TheCall.WillSucceedWithStatusCode(HttpStatusCode.Created)
            .And.TheDatabaseWillHave.Categories.EqualTo(expectedCategory);
    }
}

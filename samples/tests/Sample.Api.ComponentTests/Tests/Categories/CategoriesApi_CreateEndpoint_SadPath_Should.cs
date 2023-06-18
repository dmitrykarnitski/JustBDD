using NUnit.Framework;
using Sample.Api.Api.Models.Request;
using Sample.Api.Api.Validators;
using Sample.Api.ComponentTests.Framework.FixtureSupport;
using Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Api;

namespace Sample.Api.ComponentTests.Tests.Categories;

[TestFixture]
public class CategoriesApi_CreateEndpoint_SadPath_Should : TestFixtureBase
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void FailWithValidationErrorIfCategoryNameIsInvalid(string? categoryName)
    {
        var request = new CreateCategoryRequestBuilder()
            .WithRandomValues()
            .WithName(categoryName)
            .Build();

        Given.IHave.LoggedInAs.AValidUser();
        When.ICall.TheCategoriesApi.CreateEndpoint(request);
        Then.TheCall.WillFailWith.ValidationError(
            ValidationMessages.PropertyNotNullOrEmpty,
            nameof(CreateCategoryRequest.Name));
    }
}

using NUnit.Framework;
using Sample.Api.Api.Models.Request;
using Sample.Api.Api.Validators;
using Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;
using Sample.Api.Tests.InMemory.Integration.TestHelpers.Builders.Categories.Api;

namespace Sample.Api.Tests.InMemory.Integration.Tests.Categories;

public class CategoriesApi_CreateEndpoint_SadPath_Should : TestFixtureBase
{
    [Test]
    public void FailWithAuthenticationErrorIfTokenIsExpired()
    {
        var request = new CreateCategoryRequestBuilder()
            .WithRandomValues()
            .Build();

        Given.IHave.AnExpiredBearerToken();
        When.ICall.TheCategoriesApi.CreateEndpoint(request);
        Then.TheCall.WillFailWithAuthenticationError();
    }

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
        Then.TheCall.WillFailWithValidationError(
            ValidationMessages.PropertyNotNullOrEmpty,
            nameof(CreateCategoryRequest.Name));
    }
}

using NUnit.Framework;
using Sample.Api.ComponentTests.Framework.FixtureSupport;
using Sample.Api.ComponentTests.TestHelpers.Builders.Categories.Api;

namespace Sample.Api.ComponentTests.Tests.RequestPipeline;

[TestFixture]
public class RequestPipeline_AuthenticationMiddleware_Should : TestFixtureBase
{
    [Test]
    public void FailTheRequestIfTokenIsExpired()
    {
        var request = new CreateCategoryRequestBuilder()
            .WithRandomValues()
            .Build();

        Given.IHave.AnExpiredBearerToken();
        When.ICall.TheCategoriesApi.CreateEndpoint(request);
        Then.TheCall.WillFailWith.AuthenticationError();
    }

    [Test]
    public void FailTheRequestIfTokenIsEmpty()
    {
        var request = new CreateCategoryRequestBuilder()
            .WithRandomValues()
            .Build();

        Given.IHave.AnEmptyBearerToken();
        When.ICall.TheCategoriesApi.CreateEndpoint(request);
        Then.TheCall.WillFailWith.AuthenticationError();
    }
}

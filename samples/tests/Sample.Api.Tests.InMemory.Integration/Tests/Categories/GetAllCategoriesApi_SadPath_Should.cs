using NUnit.Framework;
using Sample.Api.Tests.InMemory.Integration.Framework.FixtureSupport;

namespace Sample.Api.Tests.InMemory.Integration.Tests.Categories;

public class GetAllCategoriesApiSadPathShould : TestFixtureBase
{
    [Test]
    [Description("fail with authentication error if token is expired")]
    public void FailWithAuthenticationErrorIfTokenIsExpired()
    {
        Given.IHave.AnExpiredBearerToken();
        When.ICall.TheCategoriesApi.GetAllEndpoint();
        Then.TheCall.WillFailWithAuthenticationError();
    }
}

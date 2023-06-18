using NUnit.Framework;
using Sample.Api.ComponentTests.Framework.FixtureSupport;

namespace Sample.Api.ComponentTests.Tests.RequestPipeline;

[TestFixture]
public class RequestPipeline_ErrorHandlingMiddleware_Should : TestFixtureBase
{
    [Test]
    public void CatchErrorAndReturnInternalServerErrorStatusCode()
    {
        Given.IHave.LoggedInAs.AValidUser().And.Database.IsUnavailable();
        When.ICall.AnyEndpoint();
        Then.TheCall.WillFailWith.InternalServerError();
    }
}

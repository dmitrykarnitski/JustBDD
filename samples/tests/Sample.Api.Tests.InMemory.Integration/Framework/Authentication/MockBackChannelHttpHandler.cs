using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.Tests.InMemory.Integration.Framework.Authentication.Resources;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Authentication;

public class MockBackChannelHttpHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri?.AbsoluteUri == MockAuthenticationConstants.OpenIdConfigurationUri)
        {
            var configurationJson = MockedAuthenticationResourcesReader.GetOpenIdConfiguration();

            return Task.FromResult(CreateResponseMessage(configurationJson));
        }

        if (request.RequestUri?.AbsoluteUri == MockAuthenticationConstants.JwksUri)
        {
            var jwksJson = MockedAuthenticationResourcesReader.GetJwks();

            return Task.FromResult(CreateResponseMessage(jwksJson));
        }

        throw new InvalidOperationException($"Url is not allowed for mocked back channel. Url was '{request.RequestUri}'");
    }

    private HttpResponseMessage CreateResponseMessage(string body)
    {
        var content = new StringContent(body, Encoding.UTF8, MediaTypeNames.Application.Json);
        return new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
    }
}

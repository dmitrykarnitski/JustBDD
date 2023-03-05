using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Sample.Api.Api.RequestPipeline.Authentication;
using Sample.Api.ComponentTests.Framework.BddContexts;

namespace Sample.Api.ComponentTests.Framework.Api;

internal class ApiClient
{
    private readonly Scenario _scenario;

    public ApiClient(Scenario scenario)
    {
        _scenario = scenario;
    }

    public void Get(string path)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);

        var responseMessage = SendRequest(requestMessage);

        SetResponseToScenario(responseMessage);
    }

    public void Post(string path, object content)
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Post, path);

        var serializedContent = JsonConvert.SerializeObject(content);
        requestMessage.Content = new StringContent(serializedContent, Encoding.UTF8, MediaTypeNames.Application.Json);

        var responseMessage = SendRequest(requestMessage);

        SetResponseToScenario(responseMessage);
    }

    private HttpResponseMessage SendRequest(HttpRequestMessage requestMessage)
    {
        if (_scenario.BearerToken != null)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationConstants.Scheme, _scenario.BearerToken);
        }

        return _scenario.SutHttpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
    }

    private void SetResponseToScenario(HttpResponseMessage httpResponseMessage)
    {
        _scenario.HttpResponse = httpResponseMessage;

        using var bodyAsStream = _scenario.HttpResponse.Content.ReadAsStream();
        using var reader = new StreamReader(bodyAsStream);
        _scenario.HttpResponseBody = reader.ReadToEnd();
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Sample.Api.Api.RequestPipeline.Authentication;
using Sample.Api.ComponentTests.Framework.BddContexts;

namespace Sample.Api.ComponentTests.Framework.Api;

internal class ApiClient
{
    private static readonly IDictionary<string, string?> noCustomHeaders = ImmutableDictionary<string, string?>.Empty;
    private static readonly JsonSerializerOptions defaultSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly Scenario _scenario;

    public ApiClient(Scenario scenario)
    {
        _scenario = scenario;
    }

    public void Get(string path)
    {
        Get(path, noCustomHeaders);
    }

    public void Get(string path, IDictionary<string, string?> customHeaders)
    {
        SendRequest(HttpMethod.Get, path, default, customHeaders);
    }

    public void GetById(string path, string id)
    {
        GetById(path, id, noCustomHeaders);
    }

    public void GetById(string path, string id, IDictionary<string, string?> customHeaders)
    {
        SendRequest(HttpMethod.Get, $"{path}/{id}", default, customHeaders);
    }

    public void Post(string path, object content)
    {
        Post(path, content, noCustomHeaders);
    }

    public void Post(string path, object content, IDictionary<string, string?> customHeaders)
    {
        SendRequest(HttpMethod.Post, path, content, customHeaders);
    }

    public void Put(string path, string id, object? content)
    {
        Put(path, id, content, noCustomHeaders);
    }

    public void Put(string path, string id, object? content, IDictionary<string, string?> customHeaders)
    {
        Put($"{path}/{id}", content, customHeaders);
    }

    public void Put(string path, object? content, IDictionary<string, string?> customHeaders)
    {
        SendRequest(HttpMethod.Put, path, content, customHeaders);
    }

    public void Delete(string path, string id, object? content)
    {
        Delete(path, id, content, noCustomHeaders);
    }

    public void Delete(string path, string id, object? content, IDictionary<string, string?> customHeaders)
    {
        Delete($"{path}/{id}", content, customHeaders);
    }

    public void Delete(string path, object? content, IDictionary<string, string?> customHeaders)
    {
        SendRequest(HttpMethod.Delete, path, content, customHeaders);
    }

    protected virtual void SendRequest(HttpMethod method, string path, object? content, IDictionary<string, string?> customHeaders)
    {
        using var requestMessage = new HttpRequestMessage(method, path);

        if (_scenario.BearerToken != null)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationConstants.Scheme, _scenario.BearerToken);
        }

        if (content is not null)
        {
            var serializedContent = JsonSerializer.Serialize(content, defaultSerializerOptions);
            requestMessage.Content = new StringContent(serializedContent, Encoding.UTF8, MediaTypeNames.Application.Json);
        }

        foreach (var header in customHeaders)
        {
            requestMessage.Headers.Add(header.Key, header.Value);
        }

        var response = _scenario.SutHttpClient.SendAsync(requestMessage).GetAwaiter().GetResult();

        SetResponseToScenario(response);
    }

    private void SetResponseToScenario(HttpResponseMessage httpResponseMessage)
    {
        _scenario.HttpResponse = httpResponseMessage;

        using var bodyAsStream = _scenario.HttpResponse.Content.ReadAsStream();
        using var reader = new StreamReader(bodyAsStream);

        _scenario.HttpResponseBody = reader.ReadToEnd();
    }
}

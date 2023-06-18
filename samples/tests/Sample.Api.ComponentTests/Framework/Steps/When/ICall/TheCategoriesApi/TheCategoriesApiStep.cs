using JustBDD.Core;
using Sample.Api.Api.Models.Request;
using Sample.Api.ComponentTests.Framework.Steps.Base;

namespace Sample.Api.ComponentTests.Framework.Steps.When.ICall.TheCategoriesApi;

public class TheCategoriesApiStep : Step<WhenStep>
{
    private const string Path = "/categories";

    public IAnd<WhenStep> GetAllEndpoint()
    {
        Scenario.ApiClient.Get(Path);

        return this;
    }

    public IAnd<WhenStep> GetByIdEndpoint(int id)
    {
        Scenario.ApiClient.GetById(Path, id.ToString());

        return this;
    }

    public IAnd<WhenStep> CreateEndpoint(CreateCategoryRequest request)
    {
        Scenario.ApiClient.Post(Path, request);

        return this;
    }
}

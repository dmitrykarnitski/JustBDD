using JustBDD.Core;
using Sample.Api.Api.Models.Request;
using Sample.Api.Tests.InMemory.Integration.Framework.Steps.Base;

namespace Sample.Api.Tests.InMemory.Integration.Framework.Steps.When.ICall.TheCategories;

public class TheCategoriesApiStep : Step<WhenStep>
{
    public IAnd<WhenStep> GetAllEndpoint()
    {
        Scenario.ApiClient.Get("/categories");

        return this;
    }

    public IAnd<WhenStep> GetByIdEndpoint(int id)
    {
        Scenario.ApiClient.Get("/categories/" + id);

        return this;
    }

    public IAnd<WhenStep> CreateEndpoint(CreateCategoryRequest request)
    {
        Scenario.ApiClient.Post("/categories", request);

        return this;
    }
}

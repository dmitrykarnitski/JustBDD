using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.Domain.Categories;
using Sample.Api.Tests.InMemory.Integration.Framework.MockingSupport;

namespace Sample.Api.Tests.InMemory.Integration.TestHelpers.Mocks.Categories;

public class MockedCategoriesRepository : MockedInMemoryRepository<Category, int>, ICategoriesRepository
{
    public MockedCategoriesRepository()
        :base(x => x.Id, (lastId) => lastId + 1)
    {
    }

    public Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        return Task.FromResult(Create(category));
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        Delete(id);

        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(GetAllItems());
    }

    public Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(FindById(id));
    }

    public Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        return Task.FromResult(Update(category));
    }
}

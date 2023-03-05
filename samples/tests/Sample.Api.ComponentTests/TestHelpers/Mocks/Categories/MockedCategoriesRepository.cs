using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.ComponentTests.Framework.MockingSupport;
using Sample.Api.Domain.Categories;

namespace Sample.Api.ComponentTests.TestHelpers.Mocks.Categories;

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

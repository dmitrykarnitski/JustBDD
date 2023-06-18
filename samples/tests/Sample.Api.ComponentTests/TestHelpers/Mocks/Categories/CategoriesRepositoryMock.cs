using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.ComponentTests.Framework.MockingSupport;
using Sample.Api.Domain.Categories;

namespace Sample.Api.ComponentTests.TestHelpers.Mocks.Categories;

public class CategoriesRepositoryMock : InMemoryRepositoryMockBase<Category, int>, ICategoriesRepository
{
    private bool _shouldThrowException;

    public CategoriesRepositoryMock()
        : base(x => x.Id, lastId => lastId + 1)
    {
    }

    public void WillThrowAnException()
    {
        _shouldThrowException = true;
    }

    public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        await ThrowExceptionIfNeeded();

        return Create(category);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        await ThrowExceptionIfNeeded();

        Delete(id);
    }

    public async Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        await ThrowExceptionIfNeeded();

        return GetAllItems();
    }

    public async Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        await ThrowExceptionIfNeeded();

        return FindById(id);
    }

    public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        await ThrowExceptionIfNeeded();

        return Update(category);
    }

    private Task ThrowExceptionIfNeeded()
    {
        if (_shouldThrowException)
        {
            var ex = new InvalidOperationException("Test database exception.");

            return Task.FromException(ex);
        }

        return Task.CompletedTask;
    }
}

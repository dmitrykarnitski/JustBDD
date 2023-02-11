using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.Domain.Categories;

namespace Sample.Api.Data.Categories;

internal class CategoriesRepository : ICategoriesRepository
{
    private static readonly List<Category> categories = new()
    {
        new Category
        {
            Id = 1,
            Name = "category 1",
            CreatedAtUtc = DateTime.UtcNow,
        },
        new Category
        {
            Id = 2,
            Name = "category 2",
            CreatedAtUtc = DateTime.UtcNow,
        }
    };

    public Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyCollection<Category>>(categories.ToArray());
    }

    public Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(categories.FirstOrDefault(c => c.Id == id));
    }

    public Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        var id = categories.Select(c => c.Id).OrderByDescending(c => c).First() + 1;

        category.Id = id;

        categories.Add(category);

        return Task.FromResult(category);
    }

    public Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        var (_, index) = categories.Select((c, i) => (c, i)).FirstOrDefault(t => t.c.Id == category.Id);

        categories.Insert(index, category);

        return Task.FromResult(category);
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var category = categories.FirstOrDefault(c => c.Id == id);
        if (category != null)
        {
            categories.Remove(category);
        }

        return Task.CompletedTask;
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Domain.Categories;

public interface ICategoriesRepository
{
    public Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken);

    public Task<Category?> FindByIdAsync(int id, CancellationToken cancellationToken);

    public Task<Category> CreateAsync(Category category, CancellationToken cancellationToken);

    public Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken);
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Domain.Products;

public interface IProductsRepository
{
    public Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken);

    public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);

    public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken);

    public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken);
}

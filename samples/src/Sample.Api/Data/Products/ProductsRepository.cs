using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.Domain.Products;

namespace Sample.Api.Data.Products;

internal class ProductsRepository : IProductsRepository
{
    public Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

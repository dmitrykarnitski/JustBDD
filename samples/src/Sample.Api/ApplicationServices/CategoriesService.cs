using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Sample.Api.Domain.Categories;

namespace Sample.Api.ApplicationServices;

public class CategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;

    public CategoriesService(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<IReadOnlyCollection<Category>> GetAsync(CancellationToken cancellationToken)
    {
        return await _categoriesRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.FindByIdAsync(id, cancellationToken);

        return category ?? throw new RequestedCategoryNotFoundException(id);
    }

    public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        // TODO: update input model type and use mapping
        return await _categoriesRepository.CreateAsync(category, cancellationToken);
    }

    public async Task<Category> UpdateAsync(Category categoryToUpdate, CancellationToken cancellationToken)
    {
        // TODO: update input model type and use mapping
        _ = await GetByIdAsync(categoryToUpdate.Id, cancellationToken);

        return await _categoriesRepository.UpdateAsync(categoryToUpdate, cancellationToken);
    }

    public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
    {
        await _categoriesRepository.DeleteAsync(category.Id, cancellationToken);
    }
}

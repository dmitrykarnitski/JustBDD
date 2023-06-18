using Sample.Api.Domain.Exceptions;

namespace Sample.Api.Domain.Categories;

public class CategoryNotFoundException : ItemNotFoundException
{
    public CategoryNotFoundException(int id)
        : base(typeof(Category), id)
    {
    }
}

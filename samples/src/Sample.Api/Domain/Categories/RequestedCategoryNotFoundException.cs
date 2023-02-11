using Sample.Api.Domain.Exceptions;

namespace Sample.Api.Domain.Categories;

public class RequestedCategoryNotFoundException : RequestedItemNotFoundException
{
    public RequestedCategoryNotFoundException(int id)
        : base(typeof(Category), id)
    {
    }
}

using System.Linq;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ValidationException = Sample.Api.Api.Exceptions.ValidationException;

namespace Sample.Api.Api.Controllers;

public class ApiController : ControllerBase
{
    protected IValidator<T> ValidatorFor<T>()
    {
        return HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
    }

    protected IMapper Mapper => HttpContext.RequestServices.GetRequiredService<IMapper>();

    protected void ValidateAndThrowIfNotValid<T>(T request)
    {
        var validationResult = ValidatorFor<T>().Validate(request);
        if (validationResult.IsValid)
        {
            return;
        }

        var errors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());

        throw new ValidationException(errors);
    }
}

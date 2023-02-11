using FluentValidation;
using Sample.Api.Api.Models.Request;

namespace Sample.Api.Api.Validators;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.PropertyNotNullOrEmpty);
    }
}

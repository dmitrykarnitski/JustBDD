using FluentValidation;
using Sample.Api.Api.Models.Request;

namespace Sample.Api.Api.Validators;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.PropertyNotNullOrEmpty);
    }
}

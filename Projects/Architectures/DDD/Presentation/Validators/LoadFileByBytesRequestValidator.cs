using FluentValidation;
using Infrastructure.Models.RequestModels;

namespace Presentation.Validators;

public class LoadFileByBytesRequestValidator : AbstractValidator<LoadFileByBytesRequest>
{
    public LoadFileByBytesRequestValidator()
    {
        RuleFor(r => r.BucketPath)
            .NotEmpty()
            .WithMessage($"Поле {nameof(LoadFileByPathRequest.BucketPath)} должно быть заполнено");
    }
}

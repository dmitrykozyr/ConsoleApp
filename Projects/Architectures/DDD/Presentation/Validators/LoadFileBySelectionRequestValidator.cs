using FluentValidation;
using Infrastructure.Models.RequestModels;

namespace Presentation.Validators;

public class LoadFileBySelectionRequestValidator : AbstractValidator<LoadFileBySelectionRequest>
{
    public LoadFileBySelectionRequestValidator()
    {
        RuleFor(r => r.BucketPath)
            .NotEmpty()
            .WithMessage($"Поле {nameof(LoadFileBySelectionRequest.BucketPath)} должно быть заполнено");
    }
}

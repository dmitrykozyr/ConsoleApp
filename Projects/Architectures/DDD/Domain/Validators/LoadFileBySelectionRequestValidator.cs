using Domain.Models.RequentModels;
using FluentValidation;

namespace Domain.Validators;

public class LoadFileBySelectionRequestValidator : AbstractValidator<LoadFileBySelectionRequest>
{
    public LoadFileBySelectionRequestValidator()
    {
        RuleFor(r => r.BucketPath)
            .NotEmpty()
            .WithMessage($"Поле {nameof(LoadFileBySelectionRequest.BucketPath)} должно быть заполнено");
    }
}

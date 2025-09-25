using Domain.Models.RequentModels;
using FluentValidation;

namespace Domain.Validators;

public class LoadFileBySelectionRequestValidator : AbstractValidator<LoadFileBySelectionRequest>
{
    public LoadFileBySelectionRequestValidator()
    {
        RuleFor(r => r.)
            .NotEmpty()
            .WithMessage($"Поле {nameof()} должно быть заполнено");
    }
}

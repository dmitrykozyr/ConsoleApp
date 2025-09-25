using Domain.Models.RequentModels;
using FluentValidation;

namespace Domain.Validators;

public class LoadFileByPathRequestValidator : AbstractValidator<LoadFileByPathRequest>
{
    public LoadFileByPathRequestValidator()
    {
        RuleFor(r => r.)
            .NotEmpty()
            .WithMessage($"Поле {nameof()} должно быть заполнено");
    }
}

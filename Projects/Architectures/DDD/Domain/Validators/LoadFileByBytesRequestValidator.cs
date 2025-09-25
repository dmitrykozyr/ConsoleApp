using Domain.Models.RequentModels;
using FluentValidation;

namespace Domain.Validators;

public class LoadFileByBytesRequestValidator : AbstractValidator<LoadFileByBytesRequest>
{
    public LoadFileByBytesRequestValidator()
    {
        RuleFor(r => r.)
            .NotEmpty()
            .WithMessage($"Поле {nameof()} должно быть заполнено");
    }
}

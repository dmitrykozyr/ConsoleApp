using Domain.Models.RequentModels;
using FluentValidation;

namespace Domain.Validators;

public class LoadFileByPathRequestValidator : AbstractValidator<LoadFileByPathRequest>
{
    public LoadFileByPathRequestValidator()
    {
        RuleFor(r => r.BucketPath)
            .NotEmpty()
            .WithMessage($"Поле {nameof(LoadFileByPathRequest.BucketPath)} должно быть заполнено");
    }
}

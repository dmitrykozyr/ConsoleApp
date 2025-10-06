using Domain.Models.RequestModels;
using FluentValidation;

namespace Domain.Validators;

public class LoadFileByBytesRequestValidator : AbstractValidator<LoadFileByBytesRequest>
{
    public LoadFileByBytesRequestValidator()
    {
        RuleFor(r => r.BucketPath)
            .NotEmpty()
            .WithMessage($"Поле {nameof(LoadFileByPathRequest.BucketPath)} должно быть заполнено");
    }
}

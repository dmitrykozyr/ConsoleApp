using Domain.Models.RequestModels;
using FluentValidation;

namespace Domain.Validators;

public class FileStorageRequestValidator : AbstractValidator<FileStorageRequest>
{
    public FileStorageRequestValidator()
    {
        RuleFor(r => r.Guid)
            .NotEmpty()
            .WithMessage($"Поле {nameof(FileStorageRequest.Guid)} должно быть заполнено");
    }
}

using FluentValidation;
using Infrastructure.Models.RequestModels;

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

using Domain.Models.RequentModels;
using FluentValidation;

namespace Domain.Validators;

public class FileStorageRequestValidator : AbstractValidator<FileStorageRequest>
{
    public FileStorageRequestValidator()
    {
        RuleFor(r => r.FileGuid)
            .NotEmpty()
            .WithMessage($"Поле {nameof(FileStorageRequest.FileGuid)} должно быть заполнено");
    }
}

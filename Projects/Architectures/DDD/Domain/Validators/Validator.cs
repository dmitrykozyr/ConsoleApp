using FluentValidation;

namespace Domain.Validators;

public static class Validator<TModel, TValidator>
    where TValidator : IValidator<TModel>, new()
{
    public static string? Validate(TModel model)
    {
        var validator = new TValidator();

        var validationResult = validator.Validate(model);

        if (!validationResult.IsValid)
        {
            string validationErrors = "";

            foreach (var error in validationResult.Errors)
            {
                validationErrors += error.ErrorMessage + " ";
            }

            return "Ошибки валидации: " + validationErrors;
        }

        return null;
    }
}

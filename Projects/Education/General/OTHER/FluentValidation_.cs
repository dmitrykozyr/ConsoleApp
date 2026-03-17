namespace Education.General;

public class FluentValidation_
{
    public void F1()
    {
        /*
            RuleFor(ps => ps.IncludePosMonth).InclusiveBetween(1, 12)
                .WithMessage("Количество месяцев больще должно быть пустым или в диапазоне 1-12");

            RuleFor(step => step)
                .Must(step => !string.IsNullOrWhiteSpace(step.Header))
                .WithMessage("Поле обязательное для заполнения: \"Заголовок\"");

            RuleFor(category => category)
                .Must(category => category?.Id != 0)
                .WithMessage("Поле обязательное для заполнения: \"Id\"");

            RuleFor(category => category)
                .Must(category => category?.CategoryForAdd?.LoyaltyId != 0)
                .WithMessage("Поле обязательное для заполнения: \"ID\"");

            RuleFor(promotion => promotion)
                .Must(promotion => promotion.ClientsFile is not null && promotion.Id.HasValue)
                .When(promotion => promotion.ClientSegmentations is not null)
                .WithMessage("Необходимо прикрепить файл с клиентами");
        */
    }
}

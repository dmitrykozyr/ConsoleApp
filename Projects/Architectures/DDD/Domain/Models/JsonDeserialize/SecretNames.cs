namespace Domain.Models.JsonDeserialize;

// Здесь перечислены секреты из Vault
// Имена должны в точности совпадать
public class SecretNames
{
    public int TOKEN_PASSWORD_1 { get; init; } //!

    public int TOKEN_PASSWORD_2 { get; init; }

    public int TokenPassword { get; init; }

    public int DbPassword { get; init; }
}

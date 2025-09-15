namespace Domain.Models.JsonDeserialize;

// Здесь перечислены секреты из Vault
// Имена должны в точности совпадать
public class SecretNames
{
    public int TOKEN_PASSWORD_1 { get; set; } //!

    public int TOKEN_PASSWORD_2 { get; set; }

    public int TokenPassword { get; set; }

    public int DbPassword { get; set; }
}

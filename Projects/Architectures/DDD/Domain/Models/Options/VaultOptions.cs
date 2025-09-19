namespace Domain.Models.Options;

public class VaultOptions
{
    public string? Address { get; init; }

    public string? Role { get; init; }

    public string? Secret { get; init; }

    public string? MountPath { get; init; }

    public string? SecretType { get; init; }
}

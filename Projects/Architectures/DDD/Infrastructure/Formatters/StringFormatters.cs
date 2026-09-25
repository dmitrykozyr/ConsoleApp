namespace Infrastructure.Formatters;

public static class StringFormatters
{
    public static byte[] ConvertBase64ToBytes(string base64String) =>
        string.IsNullOrEmpty(base64String)
            ? Array.Empty<byte>()
            : Convert.FromBase64String(base64String);
}

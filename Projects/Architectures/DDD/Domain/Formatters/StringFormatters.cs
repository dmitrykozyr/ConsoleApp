namespace Domain.Formatters;

public static class StringFormatters
{
    public static byte[] ConvertBase64ToBytes(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
        {
            return Array.Empty<byte>();
        }

        return Convert.FromBase64String(base64String);
    }
}

namespace NTier.Logic.Services.Models;

public class StandardResultObject
{
    public bool Success { get; set; }

    public string UserMessage { get; set; }

    public string InternalMessage { get; set; }

    public Exception? Exception { get; set; }

    public StandardResultObject()
    {
        Success         = false;
        UserMessage     = string.Empty;
        InternalMessage = string.Empty;
        Exception       = null;
    }
}

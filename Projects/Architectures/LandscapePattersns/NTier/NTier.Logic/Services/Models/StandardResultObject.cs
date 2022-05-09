namespace NTier.Logic.Services.Models
{
    public class StandardResultObject
    {
        public bool success { get; set; }
        public string userMessage { get; set; }

        // User will not see internal fields
        internal string internalMessage { get; set; }
        internal Exception exception { get; set; }

        public StandardResultObject()
        {
            success = false;
            userMessage = string.Empty;
            internalMessage = string.Empty;
            exception = null;
        }
    }
}

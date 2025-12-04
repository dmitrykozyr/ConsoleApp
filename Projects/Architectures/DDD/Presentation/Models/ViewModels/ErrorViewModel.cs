namespace Presentation.Models.ViewModels;

public class ErrorViewModel
{
    public string? RequestId { get; init; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string? ErrorMessage { get; init; }
}

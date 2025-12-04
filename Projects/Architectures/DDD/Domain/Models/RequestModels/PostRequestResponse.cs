namespace Domain.Models.RequestModels;

public class PostRequestResponse
{
    public Stream? Stream { get; set; }

    public HttpResponseMessage? Response { get; set; }
}

using Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Adapters;

public class FormFileAdapter : IFileUpload
{
    public string FileName => _formFile.FileName;

    public string ContentType => _formFile.ContentType;

    public long Length => _formFile.Length;

    private readonly IFormFile _formFile;

    public FormFileAdapter(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public Stream OpenReadStream()
    {
        Stream result = _formFile.OpenReadStream();

        return result;
    }
}

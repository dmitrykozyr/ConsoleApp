using WebApi.Models;

namespace WebApi.ViewModels;

public class IndexViewModel
{
    public IEnumerable<Phone>? Phones { get; set; }

    public IEnumerable<CompanyModel>? Companies { get; set; }
}

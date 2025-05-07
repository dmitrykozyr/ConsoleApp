namespace Microservices.Products.Profiles;

public class ProductProfile : AutoMapper.Profile
{
    public ProductProfile()
    {
        CreateMap<DB.Product, Models.Product>();
    }
}

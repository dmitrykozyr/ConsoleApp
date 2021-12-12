namespace ECommerce.Api.Products.Profiles
{
    // Настроим маппинг между Product Model и Product Entity
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Db.Product, Models.Product>();
        }
    }
}

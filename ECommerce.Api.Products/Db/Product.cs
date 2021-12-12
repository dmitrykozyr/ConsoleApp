namespace ECommerce.Api.Products.Db
{
    // В проекте два класса Product
    // Это используем для доступа к БД
    // Класс из папки Models будем возвращать из провайдера
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
    }
}

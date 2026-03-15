namespace Education.General.Collections;

public class IQueryable_
{
    /*
        Работает с коллекциями, которые хранятся в БД
        Строит запросы, которые будут выполнены на стороне БД и вернут только нужные данные
        Улучшает производительность при работе с большими коллекциями
        Преобразуется в SQL с WHERE - сразу отфильтровывает данные на стороне БД

            IQueryable<Phone> phoneIQuer = db.Phones;
            var phones = phoneIQuer.Where(p => p.Id > id).ToList();
            SELECT Id, Name FROM dbo.Phones WHERE Id > 3
    */

    class Product
    {
        public int Price { get; init; }

        public string? Name { get; init; }
    }

    class MyDbContext : IDisposable
    {
        public IQueryable<Product>? Products { get; init; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public void F2()
    {
        using (var context = new MyDbContext())
        {
            IQueryable<Product>? products = context?.Products?.Where(p => p.Price > 10);

            if (products is not null)
            {
                foreach (Product product in products)
                {
                    Console.WriteLine(product.Name);
                }
            }
        }
    }
}

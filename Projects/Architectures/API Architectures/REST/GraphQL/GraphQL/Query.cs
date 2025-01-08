using GraphQL.Data;
using GraphQL.Models;

namespace GraphQL.GraphQL;

public class Query
{
    [UseDbContext(typeof(ApiDbContext))]
    [UseProjection]
    [UseFiltering]  // Включаем фильтрацию и сортировку
    [UseSorting]
    public IQueryable<ItemList> GetList([ScopedService] ApiDbContext context)
    {
        return context.Lists;
    }

    [UseDbContext(typeof(ApiDbContext))]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ItemData> GetData([ScopedService] ApiDbContext context)
    {
        return context.Items;
    }
}

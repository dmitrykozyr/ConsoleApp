using AppToTest.FunctionalityToTest.Shopping_.Interfaces;

namespace AppToTest.FunctionalityToTest.Shopping_;

public class ShoppingCart
{
    private IDbService _dbService;

    public ShoppingCart(IDbService dbService)
    {
        _dbService = dbService;
    }

    public bool AddProduct(Product? product)
    {
        if (product is null || product.Id == 0)
        {
            return false;
        }

        _dbService.SaveItemToShoppingCart(product);

        return true;
    }

    public bool DeleteProduct(int? id)
    {
        if (id is null || id == 0)
        {
            return false;
        }

        _dbService.RemoveItemFromShoppingCart(id);

        return true;
    }
}

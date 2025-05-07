namespace AppToTest.FunctionalityToTest.Shopping_.Interfaces;

public interface IDbService
{
    bool SaveItemToShoppingCart(Product? product);

    bool RemoveItemFromShoppingCart(int? productId);
}

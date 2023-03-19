using AppToTest.FunctionalityToTest.Shopping_;
using AppToTest.FunctionalityToTest.Shopping_.Interfaces;
using Moq;
using Xunit;

namespace xUnit_
{
    // Используем InMemory БД
    public class ShoppingCartTest
    {
        // Если класс использует DependencyInjection, то для его тестирования используем Mock
        // Пространство имен Moq, а класс Mock
        // Указываем интерфейс IDbService, который инжектим в тестируемом классе
        // Теперь через Mock имеем доступ к функционалу этого класса

        public readonly Mock<IDbService> _dbServiceMock = new();

        [Fact]
        public void AddProduct_Success()
        {
            var shoppingCart = new ShoppingCart(_dbServiceMock.Object);

            var product = new Product(1, "shoes", 150);
            var result = shoppingCart.AddProduct(product);

            Assert.True(result);
            _dbServiceMock.Verify(z => z.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void AddProduct_FailBecauseOfInvalidPayload()
        {
            var shoppingCart = new ShoppingCart(_dbServiceMock.Object);

            var result = shoppingCart.AddProduct(null);

            Assert.False(result);
            _dbServiceMock.Verify(z => z.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public void RemoveProduct_Success()
        {
            var shoppingCart = new ShoppingCart(_dbServiceMock.Object);

            var product = new Product(1, "shoes", 150);
            shoppingCart.AddProduct(product);
            var deleteResul = shoppingCart.DeleteProduct(product.Id);

            Assert.True(deleteResul);
            _dbServiceMock.Verify(z => z.SaveItemToShoppingCart(It.IsAny<Product>()), Times.Once);
        }
    }
}

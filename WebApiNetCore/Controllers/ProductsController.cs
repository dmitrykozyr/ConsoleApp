using Microsoft.AspNetCore.Mvc;

// Создание контроллера: на папке Controllers ПКМ -> Add Controller -> Api Controller - Empty
namespace WebApiNetCore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "Hello";
        }
    }
}

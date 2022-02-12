using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers
{
    public class HomeController
    {
        // Для обращения к методу контроллера пишем https://localhost:44318/home/index
        public IActionResult Index()
        {
            return new ContentResult { Content = nameof(HomeController) };
        }
    }
}

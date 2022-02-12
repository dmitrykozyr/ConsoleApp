using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return new ContentResult { Content = nameof(BlogController) };
        }
    }
}

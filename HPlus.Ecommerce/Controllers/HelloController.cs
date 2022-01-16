using System.Web.Mvc;

namespace HPlus.Ecommerce.Controllers
{
    public class HelloController : Controller
    {
        // GET: Hello
        public ActionResult Index()
        {
            return View();
        }
    }
}
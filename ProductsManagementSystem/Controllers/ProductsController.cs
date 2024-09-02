using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        [Route("products")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

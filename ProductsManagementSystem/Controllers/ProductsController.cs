using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

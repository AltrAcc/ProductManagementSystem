using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class PartiesController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

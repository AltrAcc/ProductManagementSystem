using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class PartiesController : Controller
    {
        [Route("parties")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

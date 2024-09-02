using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers
{
    public class InvoicesController : Controller
    {
        [Route("/invoice")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace ProductsManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}

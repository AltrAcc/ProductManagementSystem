using Microsoft.AspNetCore.Mvc;

namespace ProductManagementSystem.Controllers
{
    [Route("[controller]")]
    public class PartiesController : Controller
    {
        [Route("[action]")]
        public IActionResult Index()
        {
            return View();
        }


        //When User Click on Create Party (open the create view)
        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductManagementSystem.Controllers
{
    [Route("[controller]")]
    public class PartiesController : Controller
    {
        private readonly IPartyService _partyService;

        public PartiesController(IPartyService partyService)
        {
            _partyService = partyService;
        }


        [Route("[action]")]
        public IActionResult Index()
        {
            List<PartyResponse> parties = _partyService.GetAllParties();
            return View(parties);
        }


        //When User Click on Create Party (open the create view)
        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PartyRequest partyRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            PartyResponse partyResponse = _partyService.AddParty(partyRequest);

            //navigate to Index() => parties/index
            return RedirectToAction("Index", "Parties");
        }
    }
}

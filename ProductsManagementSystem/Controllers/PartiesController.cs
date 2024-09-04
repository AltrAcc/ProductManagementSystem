using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Enums;
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
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PartyResponse.PartyName), SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {
            //List<PartyResponse> parties = _partyService.GetAllParties();
            //return View(parties);
            //Search
            ViewBag.SearchFields = new Dictionary<string, string>()
      {
        { nameof(PartyResponse.PartyName), "Party Name" },
        { nameof(PartyResponse.PartyCategory), "Category" }
      };
            List<PartyResponse> parties = _partyService.GetFilteredParties(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sort
            List<PartyResponse> sortedParties = _partyService.GetSortedParties(parties, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedParties);
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

        [HttpGet]
        [Route("[action]/{partyID}")]
        public IActionResult Edit(Guid partyID)
        {
            PartyResponse? partyResponse = _partyService.GetPartyById(partyID);

            if (partyResponse == null)
            {
                return RedirectToAction("Index");
            }

            PartyUpdateRequest partyUpdateRequest = partyResponse.ToPartyUpdateRequest();

            return View(partyUpdateRequest);
        }


        [HttpPost]
        [Route("[action]/{partyID}")]
        public IActionResult Edit(PartyUpdateRequest partyUpdateRequest)
        {
            PartyResponse? partyResponse = _partyService.GetPartyById(partyUpdateRequest.PartyID);

            if (partyResponse == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                PartyResponse updatedParty = _partyService.UpdateParty(partyUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(partyResponse.ToPartyUpdateRequest());
            }
        }


        [HttpGet]
        [Route("[action]/{partyID}")]
        public IActionResult Delete(Guid? partyID)
        {
            PartyResponse? partyResponse = _partyService.GetPartyById(partyID);
            if (partyResponse == null)
                return RedirectToAction("Index");

            return View(partyResponse);
        }

        [HttpPost]
        [Route("[action]/{partyID}")]
        public IActionResult Delete(PartyUpdateRequest
            partyUpdateRequest)
        {
            PartyResponse? partyResponse = _partyService.GetPartyById(partyUpdateRequest.PartyID);
            if (partyResponse == null)
                return RedirectToAction("Index");

            _partyService.DeleteParty(partyUpdateRequest.PartyID);
            return RedirectToAction("Index");
        }

    }
}

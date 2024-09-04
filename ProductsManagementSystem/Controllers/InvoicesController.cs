using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.ServiceContracts;

namespace ProductManagementSystem.Controllers
{
    [Route("[controller]/[action]")]
    public class InvoicesController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IProductAssignmentService _productAssignmentService;
        private readonly IPartyService _partyService;

        public InvoicesController(IInvoiceService invoiceService, IProductAssignmentService productAssignmentService, IPartyService partyService)
        {
            _invoiceService = invoiceService;
            _partyService = partyService;
            _productAssignmentService = productAssignmentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("{partyId}")]
        public IActionResult Create(int partyId)
        {
            var products = _productAssignmentService.GetAssignProductByPartyID(partyId);
            ViewBag.products = products.Select(p => new
            {
                ProductName = p.ProductName,
                ProductId = p.ProductID.ToString(),
                Price = p.ProductPrice.ToString() // Here change
            });
            InvoiceView invoiceView = new InvoiceView()
            {
                PartyID = partyId,
                PartyName = _partyService.GetPartyById(partyId).PartyName
            };
            return View(invoiceView);
        }

        [HttpPost]
        public IActionResult CreateInvoice([FromBody] List<InvoiceRequest> invoiceData)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            if (invoiceData == null || invoiceData.Count() == 0)
            {
                return null;
            }

            return Ok(new { Message = "Invoice created successfully!" });
        }
    }
}

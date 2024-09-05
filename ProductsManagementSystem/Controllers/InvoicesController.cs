using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //// Show the form and assigned products for a party
        //[HttpGet]
        //public IActionResult Create(int partyId)
        //{
        //    var assignedProducts = _productAssignmentService.GetAssignProductByPartyID(partyId);
        //    ViewBag.AssignedProducts = assignedProducts;
        //    ViewBag.PartyId = partyId;
        //    return View();
        //    //var assignedProducts = _productAssignmentService.GetAssignProductByPartyID(partyId).ToList();

        //    //var viewModel = new InvoiceViewModel
        //    //{
        //    //    PartyId = partyId,
        //    //    AssignedProducts = assignedProducts,
        //    //    InvoiceResponse = null // Or set this if you have an existing invoice response
        //    //};

        //    //return View(viewModel);
        //}


        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<InvoiceResponse> allInvoices = _invoiceService.GetAllInvoice();
            return View(allInvoices);
        }

        [HttpGet]
        public IActionResult Create(int? partyId)
        {
            ViewBag.Party = _partyService.GetAllParties().Select(p => new
            {
                Text = p.PartyName,
                Value = p.PartyID.ToString()
            });
            InvoiceViewModel invoiceView;

            if (partyId.HasValue)
            {
                var products = _productAssignmentService.GetAssignProductByPartyID(partyId.Value);
                ViewBag.products = products.Select(p => new
                {
                    ProductName = p.ProductName,
                    ProductId = p.ProductID.ToString(),
                    Price = p.ProductPrice.ToString()
                });
                invoiceView = new InvoiceViewModel()
                {
                    PartyId = partyId.Value,
                    PartyName = _partyService.GetPartyById(partyId.Value).PartyName
                };
            }
            else
            {
                invoiceView = new InvoiceViewModel();
                ViewBag.Products = Enumerable.Empty<SelectListItem>();
            }
            return View(invoiceView);
        }


        [HttpPost]
        public IActionResult CreateInvoice([FromBody] List<InvoiceRequest> invoiceData)
        {
            Console.WriteLine("Enter in Controller");
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid invoice data. ");
            }
            Console.WriteLine(invoiceData);
            if (invoiceData == null || invoiceData.Count() == 0)
            {
                Console.WriteLine(invoiceData.Count());
                return BadRequest("Invalid invoice data. ");
            }

            InvoiceResponse invoiceResponse = _invoiceService.AddInvoice(invoiceData, invoiceData[0].PartyId);

            return Ok(new { Message = "Invoice created successfully!", Total = invoiceResponse.Total.ToString(), TotalItem = invoiceResponse.ProductCount.ToString() });
        }

        [HttpGet]
        public IActionResult Details(int invoiceId)
        {
            if (invoiceId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(invoiceId));
            }

            InvoiceViewModel invoiceDetail = _invoiceService.GetInvoiceDetailsByInvoiceId(invoiceId);

            return View(invoiceDetail);
        }

        public IActionResult InvoiceOfParty(int partyId)
        {
            if (partyId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(partyId));
            }

            IEnumerable<InvoiceResponse> invoiceDetsilByParty = _invoiceService.GetInvoiceByPartyId(partyId);

            return View(invoiceDetsilByParty);
        }
    }

 }


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.ServiceContracts;
using ProductsManagementSystem.Services;
using Rotativa.AspNetCore;

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
            InvoiceView invoiceView;

            if (partyId.HasValue)
            {
                var products = _productAssignmentService.GetAssignProductByPartyID(partyId.Value);
                ViewBag.products = products.Select(p => new
                {
                    ProductName = p.ProductName,
                    ProductId = p.ProductID.ToString(),
                    Price = p.ProductPrice.ToString()
                });
                invoiceView = new InvoiceView()
                {
                    PartyID = partyId.Value,
                    PartyName = _partyService.GetPartyById(partyId.Value).PartyName
                };
            }
            else
            {
                invoiceView = new InvoiceView();
                ViewBag.Products = Enumerable.Empty<SelectListItem>();
            }
            return View(invoiceView);
        }

        [HttpGet]
        public IActionResult Delete(int invoiceId)
        {
            InvoiceResponse reponse = _invoiceService.GetInvoiceByInvoiceId(invoiceId);
            return View(reponse);
        }

        [HttpPost]
        public IActionResult Delete(int invoiceId, InvoiceResponse invoiceResponse)
        {
            if (invoiceId <= 0)
            {
                return RedirectToAction(nameof(InvoicesController.Index), "Invoices");
            }
            _invoiceService.DeleteInvoice(invoiceId);
            return RedirectToAction(nameof(InvoicesController.Index), "Invoices");
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

        [Route("InvoicePDF")]
        public async Task<IActionResult> InvoicesPDF(int invoiceId)
        {
            //Get Invoice
            InvoiceViewModel invoiceDetail = _invoiceService.GetInvoiceDetailsByInvoiceId(invoiceId);

            //Return View as PDF
            return new ViewAsPdf("InvoicePDF", invoiceDetail, ViewData)
            {
                PageMargins = new Rotativa.AspNetCore.Options.Margins()
                {
                    Top = 20,
                    Right = 20,
                    Bottom = 20,
                    Left = 20,
                },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,

            };
        }
    }

 }


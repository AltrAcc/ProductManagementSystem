using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            return View();
        }

        // Show the form and assigned products for a party
        [HttpGet]
        public IActionResult Create(int partyId)
        {
            var assignedProducts = _productAssignmentService.GetAssignProductByPartyID(partyId);
            ViewBag.AssignedProducts = assignedProducts;
            ViewBag.PartyId = partyId;
            return View();
            //var assignedProducts = _productAssignmentService.GetAssignProductByPartyID(partyId).ToList();

            //var viewModel = new InvoiceViewModel
            //{
            //    PartyId = partyId,
            //    AssignedProducts = assignedProducts,
            //    InvoiceResponse = null // Or set this if you have an existing invoice response
            //};

            //return View(viewModel);
        }


        // Handle form submission and generate invoice
        [HttpPost]
        public IActionResult Create(List<InvoiceRequest> invoiceRequests)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Generate the invoice using the service
                    var invoiceResponse = _invoiceService.Create(invoiceRequests);

                    // Pass the invoice data to the view
                    ViewBag.InvoiceResponse = invoiceResponse;
                }
                catch (Exception ex)
                {
                    // Handle errors
                    ModelState.AddModelError("", ex.Message);
                }
            }

            // Re-fetch assigned products for the view in case of validation errors
            var assignedProducts = _productAssignmentService.GetAssignProductByPartyID(invoiceRequests.First().PartyId);
            ViewBag.AssignedProducts = assignedProducts;
            ViewBag.PartyId = invoiceRequests.First().PartyId;

            return View("Create");
            //if (invoiceData == null || !invoiceData.Any())
            //{
            //    // Handle the case where no data is submitted
            //    return RedirectToAction("Create");
            //}

            //var invoiceResponse = _invoiceService.Create(invoiceData);

            //var viewModel = new InvoiceViewModel
            //{
            //    PartyId = invoiceData.First().PartyId,
            //    AssignedProducts = _productAssignmentService.GetAssignProductByPartyID(invoiceData.First().PartyId).ToList(),
            //    InvoiceResponse = invoiceResponse
            //};

            //return View(viewModel);
        }
    }

 }


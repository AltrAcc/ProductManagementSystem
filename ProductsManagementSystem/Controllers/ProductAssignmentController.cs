using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Controllers
{
    [Route("[controller]")]
    public class ProductAssignmentController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductAssignmentService _productAssignmentService;

        public ProductAssignmentController(IProductService productService, IProductAssignmentService productAssignmentService)
        {
            _productService = productService;
            _productAssignmentService = productAssignmentService;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ProductAssign(int partyId)
        {
            IEnumerable<ProductAddResponse> products = _productAssignmentService.GetNotAssignedProduct(partyId);
            ViewBag.Products = products.Select(p =>
             new SelectListItem
             {
                 Text = p.ProductName,
                 Value = p.ProductID.ToString()
             });
            ProductAssignmentRequest productAssignmentRequest = new ProductAssignmentRequest()
            {
                PartyID = partyId
            };
            return View(productAssignmentRequest);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ProductAssign(ProductAssignmentRequest request)
        {
            if (request == null)
            {
                return RedirectToAction("Index", "Parties");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                Console.WriteLine("Error occure");
                return View();
            }

            _productAssignmentService.AssignProductToParty(request.PartyID, request.SelectedProductId);

            return RedirectToAction("Index", "Parties");
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ProductAssignmentList()
        {
            IEnumerable<ProductAssignmentResponse> ProductAndPartyAssignData = _productAssignmentService.GetAllAssignProductAndParty();
            return View(ProductAndPartyAssignData);
        }
    }
}

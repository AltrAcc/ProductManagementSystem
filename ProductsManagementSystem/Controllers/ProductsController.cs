using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;
using ProductsManagementSystem.Services;

namespace ProductManagementSystem.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            List<ProductAddResponse> products = _productService.GetAllProduct();
            return View(products);
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(ProductAddRequest productAddRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            ProductAddResponse productAddResponse = _productService.AddProduct(productAddRequest);

            //navigate to Index() => parties/index
            return RedirectToAction("Index", "Products");
        }
    }
}

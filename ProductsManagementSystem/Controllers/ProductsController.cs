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
        private readonly IProductRateService _productRateService;

        public ProductsController(IProductService productService, IProductRateService productRateService)
        {
            _productService = productService;
            _productRateService = productRateService;
        }

        [Route("[action]")]
        public IActionResult Index()
        {
            IEnumerable<ProductAddResponse> products = _productService.GetAllProduct();
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

        [Route("[action]")]
        [HttpGet]
        public IActionResult Update(int productID)
        {
            ProductAddResponse reponse = _productService.GetProductById(productID);
            return View(reponse);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Update(ProductAddResponse productAddResponse)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Erros = ModelState.Values.SelectMany(t => t.Errors).Select(temp => temp.ErrorMessage);
                return View(productAddResponse);
            }

            ProductAddResponse response = _productService.UpdateProduct(productAddResponse);

            return RedirectToAction("Index", "Products");
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Delete(int productID)
        {
            ProductAddResponse reponse = _productService.GetProductById(productID);
            return View(reponse);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Delete(int ProductId, ProductAddResponse productAddResponse)
        {
            //if (ProductId <= 0)
            //{
            //    return RedirectToAction(nameof(ProductController.Index), "Product");
            //}
            _productService.DeleteProduct(ProductId);
            return RedirectToAction("Index", "Products");
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Details(int productID)
        {
            ProductAddResponse product = _productService.GetProductById(productID);
            IEnumerable<ProductRateResponse> productRate = _productRateService.GetProductRateById(productID);

            ProductRateWithProduct productRateAndProduct = new ProductRateWithProduct()
            {
                ProductAddResponse = product,
                productRateResponse = productRate,
            };
            return View(productRateAndProduct);
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ChangeRate(ProductRateWithProduct request)
        {
            if (request.ProductRateRequest == null)
            {
                return RedirectToAction("Index", "Products");
            }
            ProductRateResponse productRateResponse = _productRateService.ChangeProductRate(request.ProductRateRequest);
            return RedirectToAction("Index", "Products", new { productID = request.ProductRateRequest.ProductId });
        }
    }
}

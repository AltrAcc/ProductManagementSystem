using Microsoft.AspNetCore.Mvc;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductManagementSystem.Controllers
{
    [Route("[controller]")]
    public class ProductRatesController : Controller
    {
        private readonly IProductRateService _productRateService;


        public ProductRatesController(IProductRateService productRateService)
        {
            _productRateService = productRateService;
        }

        [Route("[action]")]
        public IActionResult GetAllProductRates()
        {
            IEnumerable<ProductRateResponse> producatRate = _productRateService.GetProductRate();
            return View(producatRate);
        }
    }
}

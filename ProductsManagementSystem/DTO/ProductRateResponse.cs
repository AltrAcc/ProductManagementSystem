using ProductManagementSystem.Models;
using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.DTO
{
    public class ProductRateResponse
    {
        public int ProductRateId { get; set; }

        public int productId { get; set; }

        public string ProductName { get; set; }

        public decimal Rate { get; set; }

        public DateTime EffectiveDate { get; set; }
    }

}

public static class ProductRateExtension
{
    public static ProductRateResponse ToProductRateResponse(this ProductRate productRate, Product product)
    {
        return new ProductRateResponse()
        {
            ProductName = product.ProductName,
            productId = productRate.ProductID,
            ProductRateId = productRate.ProductID,
            Rate = productRate.Rate,
            EffectiveDate = productRate.EffectiveDate
        };
    }
}


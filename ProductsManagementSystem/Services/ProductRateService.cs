using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class ProductRateService : IProductRateService
    {
        private readonly ApplicationDbContext _db;

        public ProductRateService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ProductRateResponse> GetProductRate()
        {
            IEnumerable<ProductRateResponse> productRate = _db.ProductRates.Select(pr => new ProductRateResponse
            {
                productId = pr.ProductID,
                EffectiveDate = pr.EffectiveDate,
                Rate = pr.Rate,
                ProductRateId = pr.ProductID,
                ProductName = _db.Products.Where(p => p.ProductID == pr.ProductID).Select(p => p.ProductName).FirstOrDefault(),
            });

            return productRate;
        }

        public IEnumerable<ProductRateResponse> GetProductRateById(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productId));
            }
            var product = _db.Products.Find(productId);
            var productRate = _db.ProductRates.Where(product => product.ProductID == productId).Select(p => p.ToProductRateResponse(product));

            return productRate;
        }

        public ProductRateResponse ChangeProductRate(ProductRateRequest productRateRequestDTO)
        {
            if (productRateRequestDTO == null)
            {
                throw new AggregateException(nameof(productRateRequestDTO));
            }

            var productRate = new ProductRate()
            {
                ProductID = productRateRequestDTO.ProductId,
                Rate = productRateRequestDTO.Rate,
                EffectiveDate = productRateRequestDTO.EffectiveDate,
            };
            var product = _db.Products.Find(productRateRequestDTO.ProductId);
            _db.ProductRates.Add(productRate);

            _db.SaveChanges();

            return productRate.ToProductRateResponse(product);
        }
    }
}

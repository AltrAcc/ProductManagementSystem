using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IProductRateService
    {
        public IEnumerable<ProductRateResponse> GetProductRate();
        public IEnumerable<ProductRateResponse> GetProductRateById(int productId);
        public ProductRateResponse ChangeProductRate(ProductRateRequest productRateRequestDTO);
    }
}

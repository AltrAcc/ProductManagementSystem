using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IProductService
    {
        ProductAddResponse AddProduct(ProductAddRequest? request);
        IEnumerable<ProductAddResponse> GetAllProduct();

        public ProductAddResponse UpdateProduct(ProductAddResponse? response);

        public bool DeleteProduct(int ProductId);

        public ProductAddResponse GetProductById(int ProductId);
    }
}

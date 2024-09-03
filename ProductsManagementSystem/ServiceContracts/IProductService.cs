using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IProductService
    {
        ProductAddResponse AddProduct(ProductAddRequest? request);
        List<ProductAddResponse> GetAllProduct();
    }
}

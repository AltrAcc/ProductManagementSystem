using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ProductAddResponse AddProduct(ProductAddRequest? ProductAddRequest)
        {
            if (ProductAddRequest == null) throw new ArgumentNullException(nameof(ProductAddRequest));

            if (ProductAddRequest.ProductName == null) throw new ArgumentException(nameof(ProductAddRequest.ProductName));


            //Convert object from ProductAddRequest to product type
            Product product = ProductAddRequest.ToProduct();

            //Generate PartyID
            product.ProductID = Guid.NewGuid();

            //Add product object to table
            _db.Add(product);
            _db.SaveChanges();

            return product.ToProductResponse();
        }

        public List<ProductAddResponse> GetAllProduct()
        {
            return _db.Products.ToList()
                .Select(temp => ConvertProductToProductResponse(temp)).ToList();
        }

        private ProductAddResponse ConvertProductToProductResponse(Product product)
        {
            ProductAddResponse productResponse = product.ToProductResponse();
            return productResponse;
        }
    }
}

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

        public ProductAddResponse AddProduct(ProductAddRequest? productAddRequest)
        {
            if (productAddRequest == null) throw new ArgumentNullException(nameof(ProductAddResponse));

            if (productAddRequest.ProductName == null) throw new ArgumentException(nameof(productAddRequest.ProductName));


            //Convert object from ProductAddResponse to product type
            Product product = productAddRequest.ToProduct();

            //Generate PartyID
            //product.ProductID = Guid.Newint();

            //Add product object to table
            _db.Add(product);
            _db.SaveChanges();

            var productRate = new ProductRate
            {
                ProductID = product.ProductID, 
                Rate = (decimal)productAddRequest.ProductPrice, 
                EffectiveDate = DateTime.Now 
            };

            _db.ProductRates.Add(productRate);
            _db.SaveChanges();

            //return product.ToProdcutResponse(productRate);

            return product.ToProductResponse(productRate);
        }

        public bool DeleteProduct(int productID)
        {
            var product = _db.Products.Find(productID);
            if (product == null)
            {
                throw new InvalidOperationException("Product Not Found");
            }

            // Remove Price as well from ProductRate
            var productRates = _db.ProductRates.Where(pr => pr.ProductID == productID).ToList();
            _db.ProductRates.RemoveRange(productRates);

            _db.Products.Remove(product);

            _db.SaveChanges();

            return true;
        }

        public IEnumerable<ProductAddResponse> GetAllProduct()
        {
            var products = _db.Products.Select(p => new
            {
                p.ProductID,
                p.ProductName,
                p.ProductDescription,
                Rate = _db.ProductRates
                    .Where(pr => pr.ProductID == p.ProductID)
                    .OrderByDescending(pr => pr.EffectiveDate)
                    .Select(pr => pr.Rate)
                    .FirstOrDefault()
            }).ToList();

            var productAddResponse = products.Select(p => new ProductAddResponse
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.Rate
            });

            return productAddResponse;
        }

        public ProductAddResponse GetProductById(int productID)
        {
            var product = _db.Products.Find(productID);
            if (product == null)
            {
                throw new InvalidOperationException("Product Not Found");
            }

            ProductRate? productRate = _db.ProductRates.Where(p => p.ProductID == productID && p.EffectiveDate <= DateTime.Now).OrderByDescending(p => p.EffectiveDate).FirstOrDefault();

            return product.ToProductResponse(productRate);
        }

        public ProductAddResponse UpdateProduct(ProductAddResponse? ProductAddResponse)
        {
            if (ProductAddResponse == null)
            {
                throw new ArgumentNullException(nameof(ProductAddResponse));
            }

            var product = _db.Products.Find(ProductAddResponse.ProductID);
            if (product == null)
            {
                throw new InvalidOperationException("Product not found");
            }

            product.ProductName = ProductAddResponse.ProductName;
            _db.SaveChanges();
            return ProductAddResponse;
        }

        private ProductAddResponse ConvertProductToProductResponse(Product product)
        {
            ProductAddResponse productResponse = product.ToProductResponse(null);
            return productResponse;
        }
    }
}

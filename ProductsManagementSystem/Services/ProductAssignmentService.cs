using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class ProductAssignmentService : IProductAssignmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductService _productService;
        private readonly IPartyService _partyService;

        public ProductAssignmentService(ApplicationDbContext db, IProductService productService, IPartyService partyService)
        {
            _db = db;
            _productService = productService;
            _partyService = partyService;
        }

        public ProductAssignmentResponse AssignProductToParty(int partyId, int ProductId)
        {
            PartyAssignment partyAssignment = new PartyAssignment()
            {
                PartyId = partyId,
                ProductId = ProductId,
                PartyAssignmentDate = DateTime.Now
            };

            _db.PartyAssignments.Add(partyAssignment);
            _db.SaveChanges();

            return new ProductAssignmentResponse()
            {
                ProductAssignId = partyAssignment.ProductId,
                ProductId = ProductId,
                PartyID = partyId
            };
        }

        public IEnumerable<ProductAssignmentResponse> GetAllAssignProductAndParty()
        {
            var productAndPartyData = _db.PartyAssignments.Include(p => p.Product).Include(p => p.Party).Select(p => new ProductAssignmentResponse
            {
                ProductId = p.Product.ProductID,
                ProductName = p.Product.ProductName,
                PartyID = p.Party.PartyID,
                PartyName = p.Party.PartyName,
                ProductAssignId = p.PartyAssignmentId
            });

            return productAndPartyData;
        }

        public IEnumerable<ProductAddResponse> GetAssignProductByPartyID(int partyId)
        {
            var party = _db.Parties.Find(partyId);
            if (party == null)
            {
                throw new Exception("Invalid Party Id");
            }

            var partyProduct = _db.PartyAssignments
                .Where(pa => pa.PartyId == partyId)
                .Include(pa => pa.Product)
                .Select(pa => new
                {
                    Product = pa.Product,
                    ProductRate = pa.Product.ProductRates
                        .Where(pr => pr.EffectiveDate <= DateTime.Now)
                        .OrderByDescending(pr => pr.EffectiveDate)
                        .FirstOrDefault()
                })
                .AsEnumerable()
                .Select(p => p.Product.ToProductResponse(p.ProductRate));

            return partyProduct;
        }

        public IEnumerable<ProductAddResponse> GetNotAssignedProduct(int partyId)
        {
            var party = _db.Parties.Find(partyId);
            if (party == null)
            {
                throw new Exception("Invalid Party Id");
            }

            var asssignProcuts = _db.PartyAssignments
                .Where(p => p.PartyId == partyId)
                .Select(p => p.ProductId).ToList();


            var notPartyProduct = _db.Products
               .Where(pa => !asssignProcuts.Contains(pa.ProductID))
               .Select(pa => new
               {
                   Product = pa,
                   ProductRate = pa.ProductRates
                       .Where(pr => pr.EffectiveDate <= DateTime.Now)
                       .OrderByDescending(pr => pr.EffectiveDate)
                       .FirstOrDefault()
               })
               .AsEnumerable()
               .Select(p => p.Product.ToProductResponse(p.ProductRate));

            return notPartyProduct;
        }


        public ProductAddResponse GetProductById(int productId)
        {
            var product = _db.Products
                .Include(p => p.ProductRates)
                .Where(p => p.ProductID == productId)
                .Select(p => new
                {
                    Product = p,
                    ProductRate = p.ProductRates
                        .Where(pr => pr.EffectiveDate <= DateTime.Now)
                        .OrderByDescending(pr => pr.EffectiveDate)
                        .FirstOrDefault()
                })
                .AsEnumerable()
                .Select(p => p.Product.ToProductResponse(p.ProductRate))
                .FirstOrDefault();

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            return product;
        }

    }
}

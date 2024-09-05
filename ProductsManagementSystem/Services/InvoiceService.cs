using ProductManagementSystem.Models;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.DTO;
using ProductsManagementSystem.ServiceContracts;

namespace ProductsManagementSystem.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductAssignmentService _productAssignmentService;

        public InvoiceService(ApplicationDbContext db, IProductAssignmentService productAssignmentService)
        {
            _db = db;
            _productAssignmentService = productAssignmentService;
        }
        public InvoiceResponse Create(List<InvoiceRequest> invoiceData)
        {
            if (invoiceData == null || invoiceData.Count == 0)
            {
                throw new ArgumentException("Invoice data cannot be null or empty.");
            }

            // Retrieve the party details
            var party = _db.Parties.Find(invoiceData.First().PartyId);
            if (party == null)
            {
                throw new Exception("Invalid Party Id.");
            }

            // Create the invoice entity
            var invoice = new Invoice
            {
                PartyId = party.PartyID,
                InvoiceDate = DateTime.Now
            };
            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            // Initialize the total amount
            decimal totalAmount = 0;

            // Process each product in the invoice
            foreach (var item in invoiceData)
            {
                var product = _productAssignmentService.GetProductById(item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with Id {item.ProductId} not found.");
                }

                // Calculate total price for the product
                decimal productTotal = (decimal)(item.Quantity * product.ProductPrice);
                totalAmount += productTotal;

                // Create the invoice details
                var invoiceDetail = new InvoiceDetails
                {
                    InvoiceId = invoice.InvoiceId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = (int)product.ProductPrice // Assuming price is stored as an int
                };

                _db.InvoicesDetails.Add(invoiceDetail);
            }

            // Save all changes to the database
            _db.SaveChanges();

            // Return the invoice response
            return new InvoiceResponse
            {
                InvoiceId = invoice.InvoiceId,
                PartyId = party.PartyID,
                PartyName = party.PartyName,
                ProductCount = invoiceData.Count,
                Total = totalAmount
            };
        }

    }
}

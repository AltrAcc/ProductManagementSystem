namespace ProductManagementSystem.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string ProductCategory { get; set; }

        public ICollection<ProductRate> ProductRates { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}

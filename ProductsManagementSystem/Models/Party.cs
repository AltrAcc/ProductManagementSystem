namespace ProductManagementSystem.Models
{
    public class Party
    {
        public Guid PartyId { get; set; }

        public string PartyName { get; set; }

        public string Partycategory { get; set; }

        public string ContactInformation { get; set; }

        public ICollection<Product> products { get; set; }

        public ICollection<Invoice> invoices { get; set; }
    }
}

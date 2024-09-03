using ProductsManagementSystem.DTO;

namespace ProductManagementSystem.Models
{
    public class Party
    {
        public Guid PartyID { get; set; }

        public string PartyName { get; set; }

        public string PartyCategory { get; set; }

        public ICollection<Product> products { get; set; }

        public ICollection<Invoice> invoices { get; set; }

    }
}

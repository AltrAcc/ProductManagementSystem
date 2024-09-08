using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.Models
{
    public class InvoiceView
    {
        public int PartyID { get; set; }

        public string PartyName { get; set; }

        public int SelectedProductID { get; set; }

        public int SelectedPartyId { get; set; }

        //public IEnumerable<ProductAddResponse> Products { get; set; }
    }
}

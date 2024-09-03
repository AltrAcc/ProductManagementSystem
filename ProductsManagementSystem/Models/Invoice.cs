namespace ProductManagementSystem.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int PartyID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }

        public Party Party { get; set; }

        public ICollection<InvoiceDetail> InvoiceDeatilts { get; set; }
    }

}

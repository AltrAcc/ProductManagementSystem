namespace ProductsManagementSystem.DTO
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }

        public int PartyId { get; set; }

        public string PartyName { get; set; }

        public DateTime InvoiceDate { get; set; }

        public IEnumerable<InvoiceItems> invoiceItems { get; set; }
    }
}

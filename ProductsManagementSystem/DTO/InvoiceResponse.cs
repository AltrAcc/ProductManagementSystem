namespace ProductsManagementSystem.DTO
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }

        public int PartyId { get; set; }

        public string PartyName { get; set; }

        public int ProductCount { get; set; }

        public decimal Total { get; set; }
    }
}

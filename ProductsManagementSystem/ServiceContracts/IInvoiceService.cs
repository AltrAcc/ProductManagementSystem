using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IInvoiceService
    {
        public InvoiceResponse AddInvoice(IEnumerable<InvoiceRequest> invoiceRequest, int PartyId);

        public IEnumerable<InvoiceResponse> GetAllInvoice();

        public IEnumerable<InvoiceResponse> GetInvoiceByPartyId(int partyId);

        public InvoiceViewModel GetInvoiceDetailsByInvoiceId(int invoiceId);

        public bool DeleteInvoice(int invoiceId);
        InvoiceResponse GetInvoiceByInvoiceId(int invoiceId);
    }
}

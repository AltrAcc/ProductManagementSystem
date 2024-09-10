using ProductManagementSystem.Models;

namespace ProductsManagementSystem.DTO
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }

        public int PartyId { get; set; }

        public string PartyName { get; set; }

        public int ProductCount { get; set; }

        public DateTime InvoiceDate { get; set; }

        public decimal Total { get; set; }
    }

    public static class InvoiceExtensions
    {
        public static InvoiceResponse ToInvoiceResponse(this Invoice invoice, InvoiceDetails invoiceDeatils)
        {
            return new InvoiceResponse()
            {
                InvoiceId = invoice.InvoiceId,
                PartyId = invoice.PartyId,
                InvoiceDate = invoice.InvoiceDate,
                ProductCount = invoiceDeatils.Quantity
            };
        }
    }
}

using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IInvoiceService
    {
        InvoiceResponse Create(List<InvoiceRequest> invoiveData);
    }
}

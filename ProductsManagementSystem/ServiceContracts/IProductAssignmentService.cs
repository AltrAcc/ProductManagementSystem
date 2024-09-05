using ProductsManagementSystem.DTO;

namespace ProductsManagementSystem.ServiceContracts
{
    public interface IProductAssignmentService
    {
        public IEnumerable<ProductAddResponse> GetAssignProductByPartyID(int partyId);

        public ProductAssignmentResponse AssignProductToParty(int partyId, int ProductId);

        public IEnumerable<ProductAddResponse> GetNotAssignedProduct(int partyId);

        public IEnumerable<ProductAssignmentResponse> GetAllAssignProductAndParty();

        public ProductAddResponse GetProductById(int productId);
    }
}

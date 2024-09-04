namespace ProductsManagementSystem.DTO
{
    public class ProductAssignment
    {
        public PartyResponse PartyResponse { get; set; }

        public IEnumerable<ProductAddResponse> ProductAddResponse { get; set; }

        public ProductAssignmentRequest? ProductAssignmentRequest { get; set; }
    }
}

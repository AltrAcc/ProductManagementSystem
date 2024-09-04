namespace ProductsManagementSystem.DTO
{
    public class ProductAssignmentResponse
    {
        public int ProductAssignId { get; set; }

        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public int PartyID { get; set; }

        public string? PartyName { get; set; }
    }
}

namespace ProductsManagementSystem.DTO
{
    public class ProductRateWithProduct
    {
        public ProductAddResponse ProductAddResponse { get; set; }

        public IEnumerable<ProductRateResponse> productRateResponse { get; set; }

        public ProductRateRequest? ProductRateRequest { get; set; }
    }
}

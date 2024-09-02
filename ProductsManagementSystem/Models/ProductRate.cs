namespace ProductManagementSystem.Models
{
    public class ProductRate
    {
        public int ProductRateId { get; set; }
        public int ProductId { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Product Product { get; set; }
    }

}

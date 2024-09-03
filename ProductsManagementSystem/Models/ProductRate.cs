namespace ProductManagementSystem.Models
{
    public class ProductRate
    {
        public int ProductRateID { get; set; }
        public int ProductID { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }

        public Product Product { get; set; }
    }

}

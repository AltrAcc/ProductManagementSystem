using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class ProductRateRequest
    {
        [Required(ErrorMessage = "ProductId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be a positive integer greater than zero.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than zero.")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "EffectiveDate is required.")]
        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }
    }
}


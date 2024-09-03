using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class ProductRate
    {
        [Key]
        public int ProductRateID { get; set; }


        [Required(ErrorMessage = "Product Id is Required")]
        public Guid ProductID { get; set; }


        [Required(ErrorMessage = "Add Product rate")]
        [Range(0, 9999.99, ErrorMessage = "Rate must be greater than 0 and less than 10,000.")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        //[DateNotInFuture(ErrorMessage = "Effective Date cannot be in the future.")]
        public DateTime EffectiveDate { get; set; } = DateTime.Now;


        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
    }

}

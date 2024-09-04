using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Product
    {
        //[Key]
        //public int ProductID { get; set; }

        //[Required(ErrorMessage = "Add product name")]
        //public string ProductName { get; set; }

        //[Required(ErrorMessage = "Add Price")]
        //public decimal? ProductPrice { get; set; }

        //[Required(ErrorMessage = "Add Description of product")]
        //[Length(minimumLength:10, maximumLength:50, ErrorMessage = "Description Length allowed between 10 to 50")]
        //public string ProductDescription { get; set; }

        //public ICollection<ProductRate> ProductRates { get; set; }

        //public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is Required")]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Add Description of product")]
        [Length(minimumLength: 10, maximumLength: 50, ErrorMessage = "Description Length allowed between 10 to 50")]
        public string ProductDescription { get; set; }

        public ICollection<ProductRate>? ProductRates { get; set; }
        //public ICollection<PartyAssignment>? ProductAssignments { get; set; }
        //public ICollection<InvoiceDetails>? InvoiceDetails { get; set; }
    }
}

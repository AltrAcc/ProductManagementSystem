using ProductManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.Models
{
    public class PartyAssignment
    {
        [Key]
        public int PartyAssignmentId { get; set; }


        [Required(ErrorMessage = "Product is Required")]
        public int ProductId { get; set; }


        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        [Required(ErrorMessage = "Party id is Required")]
        public int PartyId { get; set; }


        [ForeignKey("PartyId")]
        public Party Party { get; set; }


        [DataType(DataType.DateTime)]
        public DateTime PartyAssignmentDate { get; set; } = DateTime.Now;
    }
}

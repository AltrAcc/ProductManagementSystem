using ProductsManagementSystem.DTO;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Party
    {
        [Key]
        public Guid PartyID { get; set; }

        [Required(ErrorMessage = "Add Party Name")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Select Category")]
        public string PartyCategory { get; set; }

        public ICollection<Product> products { get; set; }

        public ICollection<Invoice> invoices { get; set; }

    }
}

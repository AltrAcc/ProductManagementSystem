using ProductsManagementSystem.DTO;
using ProductsManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Models
{
    public class Party
    {
        [Key]
        public int PartyID { get; set; }

        [Required(ErrorMessage = "Add Party Name")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Select Category")]
        public string PartyCategory { get; set; }

        //public ICollection<Product> products { get; set; }

        //public ICollection<Invoice> invoices { get; set; }

        public ICollection<PartyAssignment>? PartyAssignments { get; set; }
        public ICollection<Invoice>? Invoices { get; set; }

    }
}

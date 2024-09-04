using ProductManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class PartyUpdateRequest
    {
        [Required(ErrorMessage = "Party ID can't be blank")]
        public Guid PartyID { get; set; }

        [Required(ErrorMessage = "Add Party Name")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Select Category")]
        public string PartyCategory { get; set; }

        public Party ToParty()
        {
            return new Party()
            {
                PartyID = PartyID,
                PartyName = PartyName,
                PartyCategory = PartyCategory
            };
        }
    }
}

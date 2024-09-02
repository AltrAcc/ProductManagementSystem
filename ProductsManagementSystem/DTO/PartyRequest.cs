using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class PartyRequest
    {
        [Required(ErrorMessage = "Party Name can't be empty")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Category is must")]
        public string PartyCategory { get; set; }

        [Required(ErrorMessage = "ContactInformation can't be empty")]
        public string ContactInformation { get; set; }

        public Party ToParty()
        {
            return new Party()
            {
                PartyName = PartyName,
                PartyCategory = PartyCategory,
                ContactInformation = ContactInformation
            };
        }
    }
}

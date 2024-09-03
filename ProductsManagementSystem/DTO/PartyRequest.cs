using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class PartyRequest
    {
        [Required(ErrorMessage = "Add Party Name")]
        public string PartyName { get; set; }

        [Required(ErrorMessage = "Select Category")]
        public string PartyCategory { get; set; }

        public Party ToParty()
        {
            return new Party()
            {
                PartyName = PartyName,
                PartyCategory = PartyCategory
            };
        }
    }
}

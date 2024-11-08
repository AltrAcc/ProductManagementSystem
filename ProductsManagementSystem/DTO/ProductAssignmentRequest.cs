﻿using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class ProductAssignmentRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Party is Not Selected")]
        public int PartyID { get; set; }

        [Required(ErrorMessage = "Select Product")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Product")]
        public int SelectedProductId { get; set; }
    }
}

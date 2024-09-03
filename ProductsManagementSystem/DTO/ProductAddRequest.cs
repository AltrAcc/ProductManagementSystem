using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class ProductAddRequest
    {
        [Required(ErrorMessage = "Add Product Name")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Add Description of product")]
        [Length(10, 50, ErrorMessage = "Description length id allowed between 10 to 50")]
        public string? ProductDescription { get; set; }

        public Product ToProduct()
        {
            return new Product()
            {
                ProductName = ProductName,
                ProductDescription = ProductDescription
            };
        }
    }
}

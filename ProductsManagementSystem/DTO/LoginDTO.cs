using System.ComponentModel.DataAnnotations;

namespace ProductsManagementSystem.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Add Email")]
        [EmailAddress(ErrorMessage = "Add proper Email Address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password Not Provided")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

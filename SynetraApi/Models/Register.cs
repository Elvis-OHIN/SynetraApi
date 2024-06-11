using System.ComponentModel.DataAnnotations;

namespace SynetraApi.Models
{
    public class Register
    {
      
        [Required(ErrorMessage = "Firstname is required")]
        public required string Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public required string Lastname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
    }
}

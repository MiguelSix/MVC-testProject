using System.ComponentModel.DataAnnotations;

namespace testProject.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string? PhoneNumber { get; set; }

    }
}

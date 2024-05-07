using System.ComponentModel.DataAnnotations;

namespace FullStack.API.Model
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]

        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, long.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public long Salary { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }

    }
}

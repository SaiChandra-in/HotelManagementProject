using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class GuestDTO
    {
        public int GuestId { get; set; }

        [Required(ErrorMessage = "Username can't be blank")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username length must be between 3 and 20 characters")]
        public string? GuestName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format")]
        public string? GuestEmail { get; set; }

        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender must be 'Male', 'Female', or 'Other'")]
        public string? Gender { get; set; }

        [StringLength(200, ErrorMessage = "Address length can't exceed 200 characters")]
        public string? Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNo { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        // [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            // ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword must match properly")]
        public string? ConfirmPassword { get; set; }

    }
}

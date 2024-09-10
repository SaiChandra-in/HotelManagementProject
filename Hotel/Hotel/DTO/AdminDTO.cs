using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DTO
{
    public class AdminDTO
    {
        public int AdminId { get; set; }

        [Required(ErrorMessage = "Username can't be blank")]
        [StringLength(20,MinimumLength=3,ErrorMessage ="Username length must be between 3 and 20 characters")]
        public string? AdminName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]

        public string? Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Confirm Password is required")]
        [Compare("Password",ErrorMessage = "Password and ConfirmPassword must match properly")]
        public string? ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role can't be blank")]
        [StringLength(15,MinimumLength =3,ErrorMessage ="string length must be between 3 and 15")]
        [RegularExpression(@"^Owner|Receptionist|Manager$", ErrorMessage = "Role must be one of the following: Admin, User, Manager")]

        public string? Role { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email address is required")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid email address format")]

        public string? Email { get; set; } = string.Empty;
    }
}

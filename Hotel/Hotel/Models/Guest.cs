using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        public string? GuestName { get; set; }

        [EmailAddress(ErrorMessage ="Email address must be in (xxxx@xxx.com) form.")]
        public string? GuestEmail { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        [Phone(ErrorMessage ="Phone number must be 10 digits")]
        public string? PhoneNo { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }


        public virtual ICollection<Reservation>? Reservations{ get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room Number can't be blank")]
        public int RoomNumber { get; set; }

        [Required(ErrorMessage = "Room type can't be blank")]
        public string? RoomType { get; set; }

        public string? RoomDescription {  get; set; }
        public string? RoomStatus { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}

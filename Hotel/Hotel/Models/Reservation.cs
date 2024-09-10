using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hotel.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("Guest")]
        public int GuestId {  get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int NumberOfAdults { get; set; }
        
        public int NumberOfChildren { get; set; }

        public string? ReservationStatus { get; set; }

        public double? Price { get; set; }

        public double? Taxes { get; set; }

        public double? SeviceCost { get; set; }

        public double? TotalAmount { get; set; }

        public DateTime? CreatedAt { get; set; } 
        public DateTime? CanceledAt { get; set; }

        [JsonIgnore]
        public virtual Guest? Guest { get; set; }
        
        [JsonIgnore]
        public virtual Room? Room { get; set; }

        [JsonIgnore]
        public virtual Payment? Payment { get; set; }
    }
}

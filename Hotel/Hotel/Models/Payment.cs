using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hotel.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId {  get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        public double? TotalAmount { get; set; }

        public DateTime PaymentTime { get; set; }

        public string? CardDetails { get; set; }

        [JsonIgnore]
        public virtual Reservation? Reservation { get; set; }
    }
}

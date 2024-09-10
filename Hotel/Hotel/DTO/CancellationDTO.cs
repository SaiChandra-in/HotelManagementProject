using System.ComponentModel.DataAnnotations;

namespace Hotel.DTO
{
    public class CancellationDTO
    {
        [Required(ErrorMessage = "Reservation ID is required.")]
        public int ReservationId { get; set; }

        //[Required(ErrorMessage = "Cancellation reason is required.")]
        //public string CancellationReason { get; set; }
        public DateTime? CanceledAt { get; set; }
    }
}

using System;

namespace Hotel.DTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }

        public int ReservationId { get; set; }

        public double? TotalAmount { get; set; }

        public DateTime PaymentTime { get; set; }

        public string CardDetails { get; set; }

        public string? PaymentStatus { get; set; }

    }
}

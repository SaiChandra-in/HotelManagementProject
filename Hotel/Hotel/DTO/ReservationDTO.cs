using System;

namespace Hotel.Models
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }

        public int GuestId { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public string? ReservationStatus { get; set; }

        public double? Price { get; set; }

        public double? Taxes { get; set; }

        public double? ServiceCost { get; set; }

        public double? TotalAmount { get; set; }
       // public GuestDTO Guest { get; internal set; }
        //public RoomDTO Room { get; internal set; }
    }
}

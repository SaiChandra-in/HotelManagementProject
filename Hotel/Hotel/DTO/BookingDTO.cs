using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.DTO
{
    public class BookingDTO
    {
        [Required(ErrorMessage = "Guest ID is required.")]
        public int GuestId { get; set; }

        [Required(ErrorMessage = "Room ID is required.")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Check-in date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CheckInDate { get; set; }

        [Required(ErrorMessage = "Check-out date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CheckOutDate { get; set; }

        [Required(ErrorMessage = "Number of adults is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of adults must be at least 1.")]
        public int NumberOfAdults { get; set; }

        [Required(ErrorMessage = "Number of children is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of children must be at least 0.")]
        public int NumberOfChildren { get; set; }
        
        public double? Price { get; set; }

        public double? Taxes { get; set; }

        public double? ServiceCost { get; set; }

        public double? TotalAmount { get; set; }

        [Required(ErrorMessage = "Credit card details are required.")]
        [StringLength(19, MinimumLength = 13, ErrorMessage = "Credit card number must be between 13 and 19 digits.")]
        public string CreditCardDetails { get; set; }

        [Required(ErrorMessage = "Payment time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime PaymentTime { get; set; }

        public string? PaymentStatus { get; set; }


    }
}


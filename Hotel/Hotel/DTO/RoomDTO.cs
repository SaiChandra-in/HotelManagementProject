using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class RoomDTO
    {
        public int RoomId { get; set; }

        public int RoomNumber { get; set; }

        [RegularExpression(@"^(Single|Doublex|Triplex)$", ErrorMessage = "Room Type must be 'Single', 'Doublex', or 'Triplex'")]
        public string RoomType { get; set; } = string.Empty;

        public string? RoomDescription { get; set; }

        public string? RoomStatus { get; set; }
    }
}

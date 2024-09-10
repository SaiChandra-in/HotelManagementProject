using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;
using Hotel.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Hotel.DTO;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ReceptionistOnly")]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Select(r => new ReservationDTO
                {
                    ReservationId = r.ReservationId,
                    GuestId = r.GuestId,
                    RoomId = r.RoomId,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    NumberOfAdults = r.NumberOfAdults,
                    NumberOfChildren = r.NumberOfChildren,
                    ReservationStatus = r.ReservationStatus,
                    Price = r.Price,
                    Taxes = r.Taxes,
                    ServiceCost = r.SeviceCost,
                    TotalAmount = r.TotalAmount
                })
                .ToListAsync();

            return Ok(reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .Where(r => r.ReservationId == id)
                .Select(r => new ReservationDTO
                {
                    ReservationId = r.ReservationId,
                    GuestId = r.GuestId,
                    RoomId = r.RoomId,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    NumberOfAdults = r.NumberOfAdults,
                    NumberOfChildren = r.NumberOfChildren,
                    ReservationStatus = r.ReservationStatus,
                    Price = r.Price,
                    Taxes = r.Taxes,
                    ServiceCost = r.SeviceCost,
                    TotalAmount = r.TotalAmount
                })
                .FirstOrDefaultAsync();

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // POST: api/Reservations
        //book
        [HttpPost()]
        public async Task<IActionResult> BookRoom([FromBody] BookingDTO bookingDto)
        {
            if (bookingDto == null)
            {
                return BadRequest("Booking details are required.");
            }

            
            // Check if the check-in date and time are the same as the check-out date and time
            if (bookingDto.CheckInDate == bookingDto.CheckOutDate)
            {
                return BadRequest("Check-out datetime cannot be the same as the check-in datetime.");
            }

            if (bookingDto.CheckInDate >= bookingDto.CheckOutDate)
            {
                return BadRequest("Check-out datetime cannot be smaller than check-in datetime.");
            }

            // Check if the room exists and is available
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == bookingDto.RoomId);

            if (room == null || room.RoomStatus != "Available")
            {
                return NotFound("Room is not available.");
            }

            // Check if the guest exists
            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.GuestId == bookingDto.GuestId);

            if (guest == null)
            {
                return NotFound("Guest not found.");
            }

            // Calculate costs
            double basePrice = 100; // Example base price
            double taxes = 0.1 * basePrice; // 10% tax
            double serviceCost = 20; // Fixed service cost
            double totalAmount = basePrice + taxes + serviceCost;

            // Create reservation
            var reservation = new Reservation
            {
                GuestId = bookingDto.GuestId,
                RoomId = bookingDto.RoomId,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
                NumberOfAdults = bookingDto.NumberOfAdults,
                NumberOfChildren = bookingDto.NumberOfChildren,
                Price = bookingDto.Price,
                Taxes = bookingDto.Taxes,
                SeviceCost = bookingDto.ServiceCost,
                TotalAmount = bookingDto.TotalAmount,
                CreatedAt = DateTime.UtcNow,
                ReservationStatus = "Booked",
            };


            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            // Create payment
            var payment = new Payment
            {
                ReservationId = reservation.ReservationId,
                TotalAmount = bookingDto.TotalAmount,
                PaymentTime = bookingDto.PaymentTime,
                CardDetails = bookingDto.CreditCardDetails,
                //PaymentStatus = bookingDto.PaymentStatus
            };

            _context.Payments.Add(payment);

            // Update room status
            room.RoomStatus = "Booked";
            _context.Rooms.Update(room);

            await _context.SaveChangesAsync();

            var room1 = new Room
            {
                RoomNumber = room.RoomNumber,
                RoomType = room.RoomType,
                RoomStatus = room.RoomStatus
            };

            _context.Payments.Add(payment);

            return Ok(new
            {
                Reservation = reservation,
                Payment = payment,
                Room = room
            });
        }
        // [HttpPost]
        // public async Task<ActionResult<ReservationDTO>> PostReservation(ReservationDTO reservationDto)
        // {
        //     var reservation = new Reservation
        //     {
        //         GuestId = reservationDto.GuestId,
        //         RoomId = reservationDto.RoomId,
        //         CheckInDate = reservationDto.CheckInDate,
        //         CheckOutDate = reservationDto.CheckOutDate,
        //         NumberOfAdults = reservationDto.NumberOfAdults,
        //         NumberOfChildren = reservationDto.NumberOfChildren,
        //         ReservationStatus = reservationDto.ReservationStatus,
        //         Price = reservationDto.Price,
        //         Taxes = reservationDto.Taxes,
        //         SeviceCost = reservationDto.ServiceCost,
        //         TotalAmount = reservationDto.TotalAmount
        //     };

        //     _context.Reservations.Add(reservation);
        //     await _context.SaveChangesAsync();

        //     reservationDto.ReservationId = reservation.ReservationId;

        //     return CreatedAtAction(nameof(GetReservation), new { id = reservationDto.ReservationId }, reservationDto);
        // }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationDTO reservationDto)
        {
            if (id != reservationDto.ReservationId)
            {
                return BadRequest();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            reservation.GuestId = reservationDto.GuestId;
            reservation.RoomId = reservationDto.RoomId;
            reservation.CheckInDate = reservationDto.CheckInDate;
            reservation.CheckOutDate = reservationDto.CheckOutDate;
            reservation.NumberOfAdults = reservationDto.NumberOfAdults;
            reservation.NumberOfChildren = reservationDto.NumberOfChildren;
            reservation.ReservationStatus = reservationDto.ReservationStatus;
            reservation.Price = reservationDto.Price;
            reservation.Taxes = reservationDto.Taxes;
            reservation.SeviceCost = reservationDto.ServiceCost;
            reservation.TotalAmount = reservationDto.TotalAmount;

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] CancellationDTO cancellationDto)
        {
            if (cancellationDto == null)
            {
                return BadRequest("Cancellation details are required.");
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == cancellationDto.ReservationId);

            if (reservation == null)
            {
                return NotFound("Reservation not found.");
            }

            if (reservation.ReservationStatus != "Booked")
            {
                return BadRequest("Reservation cannot be canceled.");
            }

            // Find and remove the payment related to this reservation
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.ReservationId == reservation.ReservationId);

            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            // Update the room status to available
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == reservation.RoomId);

            if (room != null)
            {
                room.RoomStatus = "Available";
                _context.Rooms.Update(room);
            }

            // Update reservation status to canceled
            reservation.ReservationStatus = "Canceled";
            reservation.CanceledAt = DateTime.UtcNow;
            _context.Reservations.Update(reservation);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Booking canceled successfully.",
                Reservation = reservation
            });
        }
        [HttpGet("history/bookings/{year}/{month}")]
        //[Authorize(Roles = "Owner")] // Restrict access to Owner role
        public async Task<IActionResult> GetBookingHistoryByMonth(int year, int month)
        {
            // Validate the year and month
            if (year < 2000 || month < 1 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }

            // Get bookings for the specified year and month
            var reservations = await _context.Reservations
                .Where(r => r.CreatedAt.Value.Year == year && r.CreatedAt.Value.Month == month)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(new
            {
                Year = year,
                Month = month,
                Reservations = reservations
            });
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == reservation.RoomId);

            if (room != null)
            {
                room.RoomStatus = "Available";
                _context.Rooms.Update(room);
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}

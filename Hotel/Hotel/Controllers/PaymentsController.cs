using Microsoft.AspNetCore.Mvc;
using Hotel.DTOs;
using Hotel.Models; // The namespace for your EF Core entities
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Hotel.DTO;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // Your EF Core DbContext

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            var payments = await _context.Payments
                .Select(p => new PaymentDTO
                {
                    PaymentId = p.PaymentId,
                    ReservationId = p.ReservationId,
                    TotalAmount = p.TotalAmount,
                    PaymentTime = p.PaymentTime,
                    CardDetails = p.CardDetails
                })
                .ToListAsync();

            return Ok(payments);
        }

        // GET: api/payments/5
        [Authorize(Policy = "ReceptionistOnly")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(int id)
        {
            var payment = await _context.Payments
                .Where(p => p.PaymentId == id)
                .Select(p => new PaymentDTO
                {
                    PaymentId = p.PaymentId,
                    ReservationId = p.ReservationId,
                    TotalAmount = p.TotalAmount,
                    PaymentTime = p.PaymentTime,
                    CardDetails = p.CardDetails
                })
                .FirstOrDefaultAsync();

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // POST: api/payments
        [HttpPost]
        [Authorize(Policy = "ReceptionistOnly")]
        public async Task<ActionResult<PaymentDTO>> CreatePayment(PaymentDTO paymentDTO)
        {
            if (paymentDTO == null)
            {
                return BadRequest("Payment data is null");
            }

            var payment = new Payment
            {
                PaymentId = paymentDTO.PaymentId,
                ReservationId = paymentDTO.ReservationId,
                TotalAmount = paymentDTO.TotalAmount,
                PaymentTime = paymentDTO.PaymentTime,
                CardDetails = paymentDTO.CardDetails
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            paymentDTO.PaymentId = payment.PaymentId; // Set the ID for the created DTO
            return CreatedAtAction(nameof(GetPayment), new { id = paymentDTO.PaymentId }, paymentDTO);
        }

        // PUT: api/payments/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ReceptionistOnly")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentDTO paymentDTO)
        {
            if (id != paymentDTO.PaymentId)
            {
                return BadRequest("Payment ID mismatch");
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            payment.ReservationId = paymentDTO.ReservationId;
            payment.TotalAmount = paymentDTO.TotalAmount;
            payment.PaymentTime = paymentDTO.PaymentTime;
            payment.CardDetails = paymentDTO.CardDetails;
            //payment.PaymentStatus = paymentDTO.PaymentStatus;

            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/payments/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ReceptionistOnly")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return Content("Removed Successfully");
        }
//--> Owner
        [HttpGet("history/payments/{year}/{month}")]
        //[Authorize(Roles = "Owner")] // Restrict access to Owner role
        public async Task<IActionResult> GetPaymentHistoryByMonth(int year, int month)
        {
            // Validate the year and month
            if (year < 2000 || month < 1 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }

            // Get bookings for the specified year and month
            var payments = await _context.Payments
                .Where(p => p.PaymentTime.Year == year && p.PaymentTime.Month == month)
                .OrderByDescending(p => p.PaymentTime)
                .ToListAsync();

            return Ok(new
            {
                Year = year,
                Month = month,
                Payments = payments
            });
        }

    }
}

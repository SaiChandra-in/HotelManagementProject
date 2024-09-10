using Hotel.DTO;
using Hotel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequestDTO request)
        {
            // Simulate payment processing logic
            if (string.IsNullOrWhiteSpace(request.CreditCardNumber) || request.Amount <= 0)
            {
                return BadRequest(new PaymentResponse { Success = false, Message = "Invalid payment details" });
            }

            // Simulate successful payment
            return Ok(new PaymentResponse { Success = true, Message = "Payment processed successfully" });
        }
    }
}


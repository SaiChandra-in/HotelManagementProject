using System.ComponentModel.DataAnnotations;

namespace Hotel.DTO
{
    public class PaymentRequestDTO
    {
        public decimal Amount { get; set; }

        //[RegularExpression("^[0-9]{16}$")]
        public string CreditCardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        [RegularExpression("^[0-9]{3}$")]
        public int CVV { get; set; }
    }
    public class PaymentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}

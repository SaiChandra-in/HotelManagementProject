using System.ComponentModel.DataAnnotations;

namespace Hotel.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        public string? StaffName { get; set; }

        public int Age { get; set; }

        public string? Address { get; set; }

        public double? Salary { get; set; }

        public string? Designation { get; set; }

        [EmailAddress(ErrorMessage ="Email id must be in (xxxx@xxx.com) format")]
        public string? StaffEmail { get; set; }

        public string? StaffCode { get; set; }
    }
}

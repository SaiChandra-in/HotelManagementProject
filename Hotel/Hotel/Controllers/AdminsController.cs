using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.DatabaseContext;
using Hotel.Models;
using Hotel.DTO;
using Hotel.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly Password_Hash _password_Hash;
        private readonly IConfiguration _configuration;
        private readonly EmailSending _emailsending;
        public AdminsController(ApplicationDbContext context, Password_Hash passwordHasher, IConfiguration configuration, EmailSending emailsending)
        {
            _context = context;
            _password_Hash = passwordHasher;
            _configuration = configuration;
            _emailsending = emailsending;

        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> PostLogin([FromBody] LoginDTO loginDto)
        {


            if (loginDto == null)
            {
                return BadRequest("User data is null");
            }

            // Fetch the user based on email
            var user = await _context.Admins
                .FirstOrDefaultAsync(u => u.Email == loginDto.Username);

            if (user != null)
            {
                // Verify the provided password with the stored hashed password
                bool isPasswordValid = _password_Hash.VerifyPassword(user.Password, loginDto.Password);

                if (isPasswordValid)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim("AdminName",user.AdminName.ToString()),
                        new Claim("AdminEmail",user.Email.ToString()),
                        new Claim(ClaimTypes.Role,user.Role.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: signIn
                    );
                    string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { Token = tokenValue, Admin = user });
                }
                else
                {
                    return Unauthorized("Invalid password");
                }

            }
            else
            {
                return Unauthorized("Invalid email");
            }


        }

        [HttpGet("history/transaction/{year}/{month}")]
        [Authorize(Roles = "Owner")] // Restrict access to Owner role
        public async Task<IActionResult> GetTransactionHistoryByMonth(int year, int month)
        {
            // Validate the year and month
            if (year < 2000 || month < 1 || month > 12)
            {
                return BadRequest("Invalid year or month.");
            }

            // Get bookings for the specified year and month
            var transactions = await _context.Payments
                .Where(r => r.PaymentTime.Year == year && r.PaymentTime.Month == month)
                .OrderByDescending(r => r.PaymentTime)
                .ToListAsync();

            return Ok(new
            {
                Year = year,
                Month = month,
                Payments = transactions
            });
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.AdminId == id);
        }
    }
}

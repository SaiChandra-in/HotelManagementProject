using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;
using Hotel.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Hotel.DTO;
using Hotel.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
//    [Authorize(Policy = "ReceptionistOnly")]
    public class GuestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Password_Hash _password_Hash;
        private readonly IConfiguration _configuration;
        private readonly EmailSending _emailsending;
        public GuestsController(ApplicationDbContext context, Password_Hash passwordHasher, IConfiguration configuration, EmailSending emailsending)
        {
            _context = context;
            _password_Hash = passwordHasher;
            _configuration = configuration;
            _emailsending = emailsending;

        }
        ///////////////////////////////////////////////////////////////////////////////////////////
        /*[HttpPost("Login")]
        public async Task<ActionResult<string>> PostLogin([FromBody] LoginDTO loginDto)
        {


            if (loginDto == null)
            {
                return BadRequest("User data is null");
            }

            // Fetch the user based on email
            var user = await _context.Guests
                .FirstOrDefaultAsync(u => u.GuestEmail == loginDto.Username);

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
                        new Claim("GuestName",user.GuestEmail.ToString()),
                        //new Claim("GuestEmail",user.GuestEmail.ToString()),
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


        }*/


        //////////////////////////////////////////////////////////////////////////////////////////
        // GET: api/Guests
        [HttpGet]
        [Authorize(Policy = "ReceptionistOnly")]
        public async Task<ActionResult<IEnumerable<GuestDTO>>> GetGuests()
        {
            var guests = await _context.Guests
                .Select(g => new GuestDTO
                {
                    GuestId = g.GuestId,
                    GuestName = g.GuestName,
                    GuestEmail = g.GuestEmail,
                    Gender = g.Gender,
                    Address = g.Address,
                    PhoneNo = g.PhoneNo,
                    Password = g.Password,
                    ConfirmPassword = g.ConfirmPassword

                })
                .ToListAsync();

            return Ok(guests);
        }

        // GET: api/Guests/5
        [Authorize(Policy = "ReceptionistOnly")]
        [HttpGet("{GuestName}")]
        public async Task<ActionResult<IEnumerable<GuestDTO>>> GetGuest(string GuestName)
        {
            var guest = await _context.Guests
                .Where(g => g.GuestName == GuestName)
                .Select(g => new GuestDTO
                {
                    GuestId = g.GuestId,
                    GuestName = g.GuestName,
                    GuestEmail = g.GuestEmail,
                    Gender = g.Gender,
                    Address = g.Address,
                    PhoneNo = g.PhoneNo,
                })
                .FirstOrDefaultAsync();

            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }



        //This is without register code.
        // POST: api/Guests
        /*[HttpPost]
        public async Task<ActionResult<GuestDTO>> PostGuest(GuestDTO guestDto)
        {
            var guest = new Guest
            {
                GuestName = guestDto.GuestName,
                GuestEmail = guestDto.GuestEmail,
                Gender = guestDto.Gender,
                Address = guestDto.Address,
                PhoneNo = guestDto.PhoneNo,
                
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            guestDto.GuestId = guest.GuestId;

            return CreatedAtAction(nameof(GetGuest), new { id = guestDto.GuestId }, guestDto);
        }*/

        [HttpPost("Register")]
        public async Task<ActionResult<AdminDTO>> PostAdmin([FromBody] GuestDTO guestDto)
        {
            if (guestDto == null)
            {
                return BadRequest("Admin Data is null");
            }
            var c = await _context.Guests
                .Where(g => g.GuestEmail == guestDto.GuestEmail).FirstOrDefaultAsync();
            if (c!=null)
            {
                return BadRequest("Email already exists");
            }
            Password_Hash a = new Password_Hash();
            string b = a.HashPassword(guestDto.Password);

            var guest = new Guest
            {
                GuestName = guestDto.GuestName,
                GuestEmail = guestDto.GuestEmail,
                Gender = guestDto.Gender,
                Address = guestDto.Address,
                PhoneNo = guestDto.PhoneNo,
                Password = b,
                ConfirmPassword = b
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
            _emailsending.Sending(guestDto.GuestEmail, guestDto.GuestName);

            var createdGuestDto = new GuestDTO
            {
                GuestId = guestDto.GuestId,
                GuestName = guestDto.GuestName,
                GuestEmail = guestDto.GuestEmail,
                Gender = guestDto.Gender,
                Address = guestDto.Address,
                PhoneNo = guestDto.PhoneNo,
                Password = guestDto.Password,
                ConfirmPassword = guestDto.ConfirmPassword
                // Password is not included in the response
            };


            return CreatedAtAction("GetGuest", new { GuestName = guest.GuestName }, createdGuestDto);
        }

        // PUT: api/Guests/5
        [Authorize(Policy = "ReceptionistOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest(int id, GuestDTO guestDto)
        {
            if (id != guestDto.GuestId)
            {
                return BadRequest();
            }

            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            guest.GuestName = guestDto.GuestName;
            guest.GuestEmail = guestDto.GuestEmail;
            guest.Gender = guestDto.Gender;
            guest.Address = guestDto.Address;
            guest.PhoneNo = guestDto.PhoneNo;
            // guest.Password = guestDto.Password;
            // guest.ConfirmPassword = guestDto.ConfirmPassword;



            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
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

        // DELETE: api/Guests/5
        [Authorize(Policy = "ReceptionistOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.GuestId == id);

            if (reservation != null)
            {
                
            	return Content("unable to delete guest since room reservation is done.");
            }

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return Content("Successfully Deleted");
        }

        
        // public async Task<IActionResult> DeleteGuest(int id)
        // {
        //     var guest = await _context.Guests.FindAsync(id);
        //     if (guest == null)
        //     {
        //         return NotFound();
        //     }
        //     var reservation = await _context.Reservations
        //         .FirstOrDefaultAsync(r => r.GuestId == id);

        //     if (reservation != null)
        //     {
        //         var room= await _context.Rooms.FirstOrDefaultAsync(r=>r.RoomId==reservation.RoomId);
        //         if (room != null){
        //             room.RoomStatus = "Available";
        //             _context.Rooms.Update(room);
		//     _context.Guests.Remove(guest);
        //             await _context.SaveChangesAsync();
        //     	    return Content("Successfully Deleted");
        //         }
        //         _context.Guests.Remove(guest);
        //         await _context.SaveChangesAsync();
        //     	return Content("Successfully Deleted");
        //     }

        //     _context.Guests.Remove(guest);
        //     await _context.SaveChangesAsync();

        //     return Content("Successfully Deleted");
        // }

        // public async Task<IActionResult> DeleteGuest(int id)
        // {
        //     var guest = await _context.Guests.FindAsync(id);
        //     if (guest == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Guests.Remove(guest);
        //     await _context.SaveChangesAsync();

        //     return Content("Successfully Deleted");
        // }

        private bool GuestExists(int id)
        {
            return _context.Guests.Any(e => e.GuestId == id);
        }
    }
}

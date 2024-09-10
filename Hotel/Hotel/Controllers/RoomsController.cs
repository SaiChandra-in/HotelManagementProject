using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Models;
using Hotel.DatabaseContext;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    //[Authorize(Policy = "ReceptionistOnly")]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        //[Authorize(Policy = "ReceptionistOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            var rooms = await _context.Rooms
                .Select(r => new RoomDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType,
                    RoomDescription = r.RoomDescription,
                    RoomStatus = r.RoomStatus
                })
                .ToListAsync();

            return Ok(rooms);
        }

        // GET: api/Rooms/5
        [HttpGet("{roomType}")]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRoom(string roomType)
        {
            var room = await _context.Rooms
                .Where(r => r.RoomType == roomType)
                .Select(r => new RoomDTO
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType,
                    RoomDescription = r.RoomDescription,
                    RoomStatus = r.RoomStatus
                })
                .ToListAsync();

            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // POST: api/Rooms
        [HttpPost]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<ActionResult<RoomDTO>> PostRoom(RoomDTO roomDto)
        {
            var room = new Room
            {
                RoomNumber = roomDto.RoomNumber,
                RoomType = roomDto.RoomType,
                RoomDescription = roomDto.RoomDescription,
                RoomStatus = roomDto.RoomStatus
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            roomDto.RoomId = room.RoomId;

            return CreatedAtAction(nameof(GetRoom), new { roomType = roomDto.RoomType }, roomDto);
        }

        // PUT: api/Rooms/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO roomDto)
        {
            if (id != roomDto.RoomId)
            {
                return BadRequest("Room ID mismatch");
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound("Room not found");
            }

            room.RoomNumber = roomDto.RoomNumber;
            room.RoomType = roomDto.RoomType;
            room.RoomDescription = roomDto.RoomDescription;
            room.RoomStatus = roomDto.RoomStatus;

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound("Room not Found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Content(" Room Deleted Successfully");
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomId == id);
        }
    }
}

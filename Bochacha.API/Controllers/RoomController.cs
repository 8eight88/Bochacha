using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bochacha.Domain;
using Bochacha.Infrastructure.Data;
using Bochacha.Infrastructure;

namespace Bochacha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly Context _context;
        private readonly RoomRepository _roomRepository;

        public RoomController(Context context)
        {
            _context = context;
            _roomRepository = new RoomRepository(_context);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomi()
        {
            //return await _context.Persons.ToListAsync();
            return await _roomRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var room = await _roomRepository.GetByIdAsync(id);
            //if (room == null)
            //{
                //return NotFound();
            //}
            return room;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(Guid id, Room room)
        {
            if (id != room.id)
            {
                return BadRequest();
            }

            await _roomRepository.UpdateAsync(room);

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(Room room)
        {
            await _roomRepository.AddAsync(room);
            return CreatedAtAction("GetRoom", new { id = room.id }, room);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            await _roomRepository.DeleteAsync(id);

            return NoContent();
        }
        private bool RoomExists(Guid id)
        {
            return _context.Rooms.Any(e => e.id == id);
        }
    }
}

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
    public class USERController : ControllerBase
    {
        private readonly Context _context;
        private readonly USERRepository _userRepository;

        public USERController(Context context)
        {
            _context = context;
            _userRepository = new USERRepository(_context);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<USER>>> Get()
        {
            //return await _context.Persons.ToListAsync();
            return await _userRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<USER>> Get(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var user = await _userRepository.GetByIdAsync(id);
            /*if (user == null)
            {
                return NotFound();
            }*/
            return user;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, USER user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<USER>> Post(USER user)
        {
            await _userRepository.AddAsync(user);
            return CreatedAtAction("GetUSER", new { id = user.id }, user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteAsync(id);

            return NoContent();
        }
        private bool USERExists(Guid id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}

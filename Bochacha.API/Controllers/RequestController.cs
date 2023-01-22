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
    public class RequestController : ControllerBase
    {
        private readonly Context _context;
        private readonly RequestRepository _requestRepository;

        public RequestController(Context context)
        {
            _context = context;
            _requestRepository = new RequestRepository(_context);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            //return await _context.Persons.ToListAsync();
            return await _requestRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var req = await _requestRepository.GetByIdAsync(id);
            //if (room == null)
            //{
            //return NotFound();
            //}
            return req;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(Guid id, Request req)
        {
            if (id != req.id)
            {
                return BadRequest();
            }

            await _requestRepository.UpdateAsync(req);

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request req)
        {
            await _requestRepository.AddAsync(req);
            return CreatedAtAction("GetRequest", new { id = req.id }, req);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReq(Guid id)
        {
            var req = await _requestRepository.GetByIdAsync(id);
            if (req == null)
            {
                return NotFound();
            }

            await _requestRepository.DeleteAsync(id);

            return NoContent();
        }
        private bool ReqExists(Guid id)
        {
            return _context.Requests.Any(e => e.id == id);
        }
    }
}

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
    public class BuildingController : ControllerBase
    {
        private readonly Context _context;
        private readonly BuildingRepository _buildingRepository;

        public BuildingController(Context context)
        {
            _context = context;
            _buildingRepository = new BuildingRepository(_context);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            //return await _context.Persons.ToListAsync();
            return await _buildingRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(Guid id)
        {
            //var person = await _context.Persons.FindAsync(id);
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null)
            {
                return NotFound();
            }
            return building;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(Guid id, Building building)
        {
            if (id != building.id)
            {
                return BadRequest();
            }

            await _buildingRepository.UpdateAsync(building);

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            await _buildingRepository.AddAsync(building);
            return CreatedAtAction("GetBuilding", new { id = building.id }, building);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(Guid id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            await _buildingRepository.DeleteAsync(id);

            return NoContent();
        }
        private bool BuildingExists(Guid id)
        {
            return _context.Buildings.Any(e => e.id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bochacha.Domain;
using Bochacha.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace Bochacha.Infrastructure
{
    public class BuildingRepository
    {
        private readonly Context? _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public BuildingRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Building>> GetAllAsync()
        {
            return await _context.Buildings.OrderBy(b => b.name).ToListAsync();
        }
        public async Task<Building> GetByIdAsync(Guid id)
        {
            return await _context.Buildings.Where(b => b.id == id)
                .Include(b => b.Rooms)
                .FirstOrDefaultAsync();
        }

        public async Task<Building> GetByNameAsync(string name)
        {
            return await _context.Buildings
                .Where(b => b.name == name)
                .Include(b => b.Rooms)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Building building)
        {
            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Building? building)
        {
            var existBuilding = GetByIdAsync(building.id).Result;
            if (existBuilding != null)
            {
                _context.Entry(existBuilding).CurrentValues.SetValues(building);
                foreach (var room in building.Rooms)
                {
                    var existRoom = existBuilding.Rooms.FirstOrDefault(br => br.id == room.id);
                    if (existRoom == null)
                    {
                        existBuilding.Rooms.Add(room);
                    }
                    else
                    {
                        _context.Entry(existRoom).CurrentValues.SetValues(room);
                    }
                }
                foreach (var existRoom in existBuilding.Rooms)
                {
                    if (!building.Rooms.Any(br => br.id == existRoom.id))
                    {
                        _context.Remove(existRoom);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            Building building = await _context.Buildings.FindAsync(id);
            _context.Remove(building);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}

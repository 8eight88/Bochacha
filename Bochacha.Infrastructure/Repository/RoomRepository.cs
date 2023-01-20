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
    public class RoomRepository
    {
        private readonly Context? _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public RoomRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms.OrderBy(ro => ro.number).ToListAsync();
        }
        public async Task<Room> GetByIdAsync(Guid id)
        {
            return await _context.Rooms.Where(ro => ro.id == id)
                .Include(ro => ro.Equipmenti)
                .FirstOrDefaultAsync();
        }

        public async Task<Room> GetByNameAsync(int number)
        {
            return await _context.Rooms
                .Where(ro => ro.number == number)
                .Include(ro => ro.Equipmenti)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Room room)
        {
            var existRoom = GetByIdAsync(room.id).Result;
            if (existRoom != null)
            {
                _context.Entry(existRoom).CurrentValues.SetValues(room);
                foreach (var equipment in room.Equipmenti)
                {
                    var existEquipment = existRoom.Equipmenti.FirstOrDefault(pn => pn.id == equipment.id);
                    if (existEquipment == null)
                    {
                        existRoom.Equipmenti.Add(equipment);
                    }
                    else
                    {
                        _context.Entry(existEquipment).CurrentValues.SetValues(equipment);
                    }
                }
                foreach (var existNumber in existRoom.Equipmenti)
                {
                    if (!room.Equipmenti.Any(rEq => rEq.id == existNumber.id))
                    {
                        _context.Remove(existNumber);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Remove(room);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}

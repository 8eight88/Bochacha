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
    public class USERRepository
    {
        private readonly Context? _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public USERRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<USER>> GetAllAsync()
        {
            return await _context.Users.OrderBy(u => u.type).ToListAsync();
        }
        public async Task<USER> GetByIdAsync(Guid id)
        {
            return await _context.Users.Where(u => u.id == id)
                .Include(u => u.Rooms)
                .Include(u => u.Requests)
                .FirstOrDefaultAsync();
        }

        public async Task<USER> GetByTypeAsync(string type)
        {
            return await _context.Users
                .Where(u => u.type == type)
                .Include(u => u.Rooms)
                .Include(u => u.Requests)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(USER user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(USER user)
        {
            var existUser = GetByIdAsync(user.id).Result;
            if (existUser != null)
            {
                _context.Entry(existUser).CurrentValues.SetValues(user);
                foreach (var room in user.Rooms)
                {
                    var existRoom = existUser.Rooms.FirstOrDefault(ur => ur.id == room.id);
                    if (existRoom == null)
                    {
                        existUser.Rooms.Add(room);
                    }
                    else
                    {
                        _context.Entry(existRoom).CurrentValues.SetValues(room);
                    }
                }
                foreach (var existRoom in existUser.Rooms)
                {
                    if (!user.Rooms.Any(ur => ur.id == existRoom.id))
                    {
                        _context.Remove(existRoom);
                    }
                }
                //++++++++++++++
                foreach (var request in user.Requests)
                {
                    var existRequest = existUser.Requests.FirstOrDefault(ure => ure.id == request.id);
                    if (existRequest == null)
                    {
                        existUser.Requests.Add(request);
                    }
                    else
                    {
                        _context.Entry(existRequest).CurrentValues.SetValues(request);
                    }
                }
                foreach (var existRequest in existUser.Requests)
                {
                    if (!user.Requests.Any(ure => ure.id == existRequest.id))
                    {
                        _context.Remove(existRequest);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            USER user = await _context.Users.FindAsync(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }


    }
}

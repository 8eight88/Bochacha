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
    public class RequestRepository
    {
        private readonly Context? _context;
        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public RequestRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<Request>> GetAllAsync()
        {
            return await _context.Requests.OrderBy(re => re.contents).ToListAsync();
        }
        public async Task<Request> GetByIdAsync(Guid id)
        {
            return await _context.Requests.FindAsync(id);

        }
        /*
        public async Task<Request> GetByNameAsync(int number)
        {
            return await _context.Rooms
                .Where(ro => ro.number == number)
                .Include(ro => ro.Equipmenti)
                .FirstOrDefaultAsync();
        }
        */

        public async Task AddAsync(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Request request)
        {
            var existReq = GetByIdAsync(request.id).Result;
            if (existReq != null)
            {
                _context.Entry(existReq).CurrentValues.SetValues(request);
                
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            Request request = await _context.Requests.FindAsync(id);
            _context.Remove(request);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}

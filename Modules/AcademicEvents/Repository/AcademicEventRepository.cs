using AcademicCalendar.Data;
using AcademicCalendar.Modules.AcademicEventModel.Model;
using Microsoft.EntityFrameworkCore;

namespace AcademicCalendar.Modules.AcademicEvents.Repository
{
    public class AcademicEventRepository
    {
        private readonly AppDbContext _context;

        public AcademicEventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicEvent>> GetByYearAsync(int yearId)
        {
            return await _context.AcademicEvents
                .Where(e => e.AcademicYearId == yearId)
                .ToListAsync();
        }

        public async Task<AcademicEvent> CreateAsync(AcademicEvent evt)
        {
            _context.AcademicEvents.Add(evt);
            await _context.SaveChangesAsync();
            return evt;
        }
    }
}
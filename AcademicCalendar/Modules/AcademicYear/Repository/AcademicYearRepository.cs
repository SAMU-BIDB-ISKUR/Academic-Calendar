using AcademicCalendar.Data;
using AcademicCalendar.Modules.AcademicYear.Model;
using Microsoft.EntityFrameworkCore;

namespace AcademicCalendar.Modules.AcademicYear.Repository
{
    public class AcademicYearRepository
    {
        private readonly AppDbContext _context;

        public AcademicYearRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicYear>> GetAllAsync()
        {
            return await _context.AcademicYears.ToListAsync();
        }

        public async Task<AcademicYear?> GetByIdAsync(int id)
        {
            return await _context.AcademicYears.FindAsync(id);
        }

        public async Task<AcademicYear> CreateAsync(AcademicYear year)
        {
            _context.AcademicYears.Add(year);
            await _context.SaveChangesAsync();
            return year;
        }
    }
}

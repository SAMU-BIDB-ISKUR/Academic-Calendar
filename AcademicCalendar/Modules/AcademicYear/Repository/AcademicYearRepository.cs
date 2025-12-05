using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcademicCalendar.Data;
using AYModel = AcademicCalendar.Modules.AcademicYear.Model.AcademicYear;

namespace AcademicCalendar.Modules.AcademicYear.Repository
{
    public class AcademicYearRepository
    {
        private readonly AppDbContext _context;

        public AcademicYearRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AYModel>> GetAllAsync()
        {
            return await _context.AcademicYears.ToListAsync();
        }

        public async Task<AYModel?> GetByIdAsync(int id)
        {
            return await _context.AcademicYears.FindAsync(id);
        }

        public async Task<AYModel> CreateAsync(AYModel year)
        {
            _context.AcademicYears.Add(year);
            await _context.SaveChangesAsync();
            return year;
        }

        public async Task<bool> UpdateAsync(AYModel year)
        {
            _context.Entry(year).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.AcademicYears.FindAsync(year.Id) == null)
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var yearToDelete = await _context.AcademicYears.FindAsync(id);
            if (yearToDelete == null)
            {
                return false;
            }

            _context.AcademicYears.Remove(yearToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

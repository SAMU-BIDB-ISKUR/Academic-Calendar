using AcademicCalendar.Data;
using AcademicCalendar.Modules.AcademicEvents.DTOs;
using AcademicCalendar.Modules.AcademicEventModel.Model;
using AcademicCalendar.Modules.AcademicEvents.Repository;
using Microsoft.EntityFrameworkCore;

namespace AcademicCalendar.Modules.AcademicEvents.Service
{
    public class AcademicEventService
    {
        private readonly AppDbContext _context;
        private readonly AcademicEventRepository _repository;

        public AcademicEventService(AppDbContext context, AcademicEventRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<AcademicEvent> CreateAsync(AcademicEventCreateDto dto)
        {
            var year = await _context.AcademicYears
                .Include(y => y.Events)
                .FirstOrDefaultAsync(y => y.Id == dto.AcademicYearId);

            return null!;
        }
    }
}

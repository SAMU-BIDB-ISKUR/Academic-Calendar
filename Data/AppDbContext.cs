using AcademicCalendar.Modules.AcademicEventModel.Model;
using AcademicCalendar.Modules.AcademicYears.Model;
using Microsoft.EntityFrameworkCore;

namespace AcademicCalendar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AcademicYearModel> AcademicYears { get; set; }
        public DbSet<AcademicEvent> AcademicEvents { get; set; }
    }
}

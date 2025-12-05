using Microsoft.EntityFrameworkCore;
using AcademicCalendar.Modules.AcademicYear.Model;

namespace AcademicCalendar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AcademicYear> AcademicYears { get; set; }
    }
}

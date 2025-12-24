using AcademicCalendar.Modules.AcademicYears.Model;

namespace AcademicCalendar.Modules.AcademicEventModel.Model
{
    public class AcademicEvent
    {
        public int Id { get; set; }
        public int AcademicYearId { get; set; }
       public virtual AcademicCalendar.Modules.AcademicYears.Model.AcademicYearModel AcademicYear { get; set; } = null!;
        public string Name { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

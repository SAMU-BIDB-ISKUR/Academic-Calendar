namespace AcademicCalendar.Modules.AcademicEvents.DTOs
{
    public class AcademicEventCreateDto
    {
        public int AcademicYearId { get; set; }
        public string Name { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

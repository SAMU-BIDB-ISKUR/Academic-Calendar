namespace AcademicCalendar.Modules.AcademicYear.DTOs
{
    public class AcademicYearCreateDto
    {
        public string YearName { get; set; } = string.Empty;
        public DateTime FallStart { get; set; }
        public DateTime FallEnd { get; set; }
        public DateTime SpringStart { get; set; }
        public DateTime SpringEnd { get; set; }
    }

    public class AcademicYearDto : AcademicYearCreateDto
    {
        public int Id { get; set; }
    }
}

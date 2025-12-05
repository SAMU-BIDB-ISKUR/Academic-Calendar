namespace AcademicCalendar.Modules.AcademicYear.Model
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string YearName { get; set; } = string.Empty;
        public DateTime FallStart { get; set; }
        public DateTime FallEnd { get; set; }
        public DateTime SpringStart { get; set; }
        public DateTime SpringEnd { get; set; }
    }
}

namespace AcademicCalendar.Modules.AcademicYears.Model
{
    public class AcademicYearModel
    {
        public int Id { get; set; }
        public string YearName { get; set; } = string.Empty;

        public DateTime FallStart { get; set; }
        public DateTime FallEnd { get; set; }

        public DateTime SpringStart { get; set; }
        public DateTime SpringEnd { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<AcademicEventModel.Model.AcademicEvent> Events { get; set; }
            = new List<AcademicEventModel.Model.AcademicEvent>();
    }
}

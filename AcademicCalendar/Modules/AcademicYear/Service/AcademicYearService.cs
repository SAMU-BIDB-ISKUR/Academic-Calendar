using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using AcademicCalendar.Modules.AcademicYear.DTOs;
using AcademicCalendar.Modules.AcademicYear.Repository;
using AYModel = AcademicCalendar.Modules.AcademicYear.Model.AcademicYear;


namespace AcademicCalendar.Modules.AcademicYear.Service
{
    public class AcademicYearService
    {
        private readonly AcademicYearRepository _repo;

        public AcademicYearService(AcademicYearRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AcademicYearDto>> GetAll()
        {
            var list = await _repo.GetAllAsync();

            return list.Select(x => new AcademicYearDto
            {
                Id = x.Id,
                YearName = x.YearName,
                FallStart = x.FallStart,
                FallEnd = x.FallEnd,
                SpringStart = x.SpringStart,
                SpringEnd = x.SpringEnd
            }).ToList();
        }

        public async Task<AcademicYearDto> Create(AcademicYearCreateDto dto)
        {
            var year = new AYModel
            {
                YearName = dto.YearName,
                FallStart = dto.FallStart,
                FallEnd = dto.FallEnd,
                SpringStart = dto.SpringStart,
                SpringEnd = dto.SpringEnd
            };

            var created = await _repo.CreateAsync(year);

            return new AcademicYearDto
            {
                Id = created.Id,
                YearName = created.YearName,
                FallStart = created.FallStart,
                FallEnd = created.FallEnd,
                SpringStart = created.SpringStart,
                SpringEnd = created.SpringEnd
            };
        }
    }
}

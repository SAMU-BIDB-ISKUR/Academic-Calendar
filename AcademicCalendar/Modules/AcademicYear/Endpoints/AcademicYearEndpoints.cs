using AcademicCalendar.Modules.AcademicYear.DTOs;
using AcademicCalendar.Modules.AcademicYear.Service;

namespace AcademicCalendar.Modules.AcademicYear.Endpoints
{
    public static class AcademicYearEndpoints
    {
        public static IEndpointRouteBuilder MapAcademicYearEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/academicyears");

            group.MapGet("/", async (AcademicYearService service) =>
            {
                return await service.GetAll();
            });

            group.MapPost("/", async (AcademicYearService service, AcademicYearCreateDto dto) =>
            {
                return await service.Create(dto);
            });

            return routes;
        }
    }
}

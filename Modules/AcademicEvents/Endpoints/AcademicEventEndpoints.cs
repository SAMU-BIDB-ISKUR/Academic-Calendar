using AcademicCalendar.Modules.AcademicEvents.DTOs;
using AcademicCalendar.Modules.AcademicEvents.Service;

namespace AcademicCalendar.Modules.AcademicEvents.Endpoints
{
    public static class AcademicEventEndpoints
    {
        public static IEndpointRouteBuilder MapAcademicEventEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/events");

            group.MapPost("/", async (AcademicEventCreateDto dto, AcademicEventService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Ok(created);
            });

            return app;
        }
    }
}

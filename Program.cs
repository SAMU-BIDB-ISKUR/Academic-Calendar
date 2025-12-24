using Microsoft.EntityFrameworkCore;
using AcademicCalendar.Data;

// AcademicYear
using AcademicCalendar.Modules.AcademicYear.Endpoints;
using AcademicCalendar.Modules.AcademicYear.Repository;
using AcademicCalendar.Modules.AcademicYear.Service;

// AcademicEvent
using AcademicCalendar.Modules.AcademicEvents.Endpoints;
using AcademicCalendar.Modules.AcademicEvents.Repository;
using AcademicCalendar.Modules.AcademicEvents.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// AcademicYear Services
builder.Services.AddScoped<AcademicYearRepository>();
builder.Services.AddScoped<AcademicYearService>();

// AcademicEvent Services
builder.Services.AddScoped<AcademicEventRepository>();
builder.Services.AddScoped<AcademicEventService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Endpoints
app.MapAcademicYearEndpoints();
app.MapAcademicEventEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

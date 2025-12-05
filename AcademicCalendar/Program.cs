using Microsoft.EntityFrameworkCore;
using AcademicCalendar.Data;
using AcademicCalendar.Modules.AcademicYear.Endpoints;
using AcademicCalendar.Modules.AcademicYear.Repository;
using AcademicCalendar.Modules.AcademicYear.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AcademicYearRepository>();
builder.Services.AddScoped<AcademicYearService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapAcademicYearEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

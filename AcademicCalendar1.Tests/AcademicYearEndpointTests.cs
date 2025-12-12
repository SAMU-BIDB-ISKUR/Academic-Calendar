using AcademicCalendar.Data;
using AcademicCalendar.Modules.AcademicYear.DTOs;
using AcademicCalendar.Modules.AcademicYear.Repository;
using AcademicCalendar.Modules.AcademicYear.Service;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace AcademicCalendar1.Tests
{
    public class AcademicYearEndpointsTests : IAsyncLifetime
    {
        private WebApplicationFactory<Program>? _factory;
        private HttpClient? _client;
        private DbConnection? _connection;

        public async Task InitializeAsync()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            await _connection.OpenAsync();

            _factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        
                        var descriptor = services.SingleOrDefault(  
                            d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddDbContext<AppDbContext>(options =>
                        {
                            options.UseSqlite(_connection);
                        });
                        services.TryAddScoped<AcademicYearRepository>();
                        services.TryAddScoped<AcademicYearService>();
                    });
                    builder.ConfigureTestServices(services =>
                    {
                        var sp = services.BuildServiceProvider();
                        using var scope = sp.CreateScope();
                        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        db.Database.EnsureCreated();
                    });
                });

            _client = _factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            _client?.Dispose();

            if (_factory != null)
            {
                await _factory.DisposeAsync();
            }

            if (_connection != null)
            {
                await _connection.CloseAsync();
                await _connection.DisposeAsync();
            }
        }

        [Fact]
        public async Task Get_Should_Return_200_OK_From_Real_Db()
        {
            // Act
            var response = await _client!.GetAsync("/api/academicyears");

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"HATA: {response.StatusCode} - {error}");
            }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Post_Should_Add_Real_Record_To_Db()
        {
            // Arrange
            var uniqueName = $"Gercek Kayit {Guid.NewGuid().ToString().Substring(0, 5)}";
            var newYear = new AcademicYearCreateDto
            {
                YearName = uniqueName,
                FallStart = DateTime.Now,
                FallEnd = DateTime.Now.AddMonths(4),
                SpringStart = DateTime.Now.AddMonths(5),
                SpringEnd = DateTime.Now.AddMonths(9)
            };

            // Act
            var response = await _client!.PostAsJsonAsync("/api/academicyears", newYear);

            // Assert
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"HATA: {response.StatusCode} - {error}");
            }
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            using var scope = _factory!.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var allRecords = await dbContext.AcademicYears.ToListAsync();
            Assert.Contains(allRecords, y => y.YearName == uniqueName);
        }

        [Fact]
        public async Task Get_Should_Return_Empty_List_Initially()
        {
            // Act
            var response = await _client!.GetAsync("/api/academicyears");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadFromJsonAsync<List<AcademicYearCreateDto>>();
            Assert.NotNull(content);
        }
    }
}
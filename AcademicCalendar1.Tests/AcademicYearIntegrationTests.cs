using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Xunit; // xUnit kullandığınızı varsayıyorum

// Projenizin gerçek namespace'lerini buraya ekliyoruz
using AcademicCalendar.Data; 
using AcademicCalendar.Modules.AcademicYear.DTOs;
using AcademicCalendar.Modules.AcademicYear.Repository;
using AcademicCalendar.Modules.AcademicYear.Service;
using AcademicCalendar.Modules.AcademicYear.Model;

namespace AcademicCalendar1.Tests
{
    public class AcademicYearIntegrationTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly AppDbContext _context;
        private readonly AcademicYearRepository _repository;
        private readonly AcademicYearService _service;

        public AcademicYearIntegrationTests()
        {
            // 1. SQLite bağlantısını bellekte (memory) açıyoruz
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // 2. DbContext ayarlarını SQLite kullanacak şekilde yapılandırıyoruz
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            // 3. Context'i ve tablolar oluşturuyoruz
            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            
            _repository = new AcademicYearRepository(_context);
            _service = new AcademicYearService(_repository);
        }

        [Fact]
        public async Task Create_Should_Add_New_AcademicYear_To_Database()
        {
            // Arrange (Hazırlık)
            var dto = new AcademicYearCreateDto
            {
                YearName = "2024-2025",
                FallStart = DateTime.Now,
                FallEnd = DateTime.Now.AddMonths(4),
                SpringStart = DateTime.Now.AddMonths(5),
                SpringEnd = DateTime.Now.AddMonths(9)
            };

            // Act (Eylem)
            var result = await _service.Create(dto);

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.True(result.Id > 0); 
            Assert.Equal("2024-2025", result.YearName);
            var count = await _context.AcademicYears.CountAsync();
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task GetAll_Should_Return_All_AcademicYears()
        {
            // Arrange (Hazırlık)
            _context.AcademicYears.Add(new AcademicCalendar.Modules.AcademicYear.Model.AcademicYear 
            { 
                YearName = "2023-2024",
                FallStart = DateTime.Now, FallEnd = DateTime.Now, 
                SpringStart = DateTime.Now, SpringEnd = DateTime.Now 
            });
            
            _context.AcademicYears.Add(new AcademicCalendar.Modules.AcademicYear.Model.AcademicYear 
            { 
                YearName = "2024-2025",
                FallStart = DateTime.Now, FallEnd = DateTime.Now, 
                SpringStart = DateTime.Now, SpringEnd = DateTime.Now 
            });
            
            await _context.SaveChangesAsync();

            // Act (Eylem)
            var result = await _service.GetAll();

            // Assert (Doğrulama)
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, x => x.YearName == "2023-2024");
            Assert.Contains(result, x => x.YearName == "2024-2025");
        }

        public void Dispose()
        {
            // baglantıyı temizle
            _connection.Close();
            _connection.Dispose();
            _context.Dispose();
        }
    }

}
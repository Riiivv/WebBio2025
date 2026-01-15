using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Infrastucture;
using WebBio2025.Infrastucture.Repositories;

namespace WebBio2025.Test.Repositories
{
    public class HallRepositoryTest
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;

        public HallRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _context = new DatabaseContext(_options);

            _context.Halls.AddRange(
                new Hall { HallId = 1, HallNumber = 1, Capacity = 100 },
                new Hall { HallId = 2, HallNumber = 2, Capacity = 200 }
            );

            _context.SaveChanges();
        }

        [Fact]
        public async Task GetHallById_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var repo = new HallRepository(_context);

            // Act
            var result = await repo.GetHallById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetHallById_ReturnsHall_WhenFound()
        {
            // Arrange
            var repo = new HallRepository(_context);

            // Act
            var result = await repo.GetHallById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.HallId);
            Assert.Equal(1, result.HallNumber);
            Assert.Equal(100, result.Capacity);
        }

        [Fact]
        public async Task GetAllHalls_ReturnsAll()
        {
            // Arrange
            var repo = new HallRepository(_context);

            // Act
            var result = await repo.GetAllHalls();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateHall_CreatesHall()
        {
            // Arrange
            var repo = new HallRepository(_context);

            var newHall = new Hall
            {
                HallNumber = 3,
                Capacity = 300
            };

            // Act
            var result = await repo.CreateHall(newHall);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HallId > 0);
            Assert.Equal(3, result.HallNumber);
            Assert.Equal(300, result.Capacity);

            var all = await repo.GetAllHalls();
            Assert.Equal(3, all.Count);
        }

        [Fact]
        public async Task UpdateHall_ReturnsNull_WhenNotFound()
        {
            // Arrange
            var repo = new HallRepository(_context);

            var update = new Hall
            {
                HallId = 999,
                HallNumber = 99,
                Capacity = 999
            };

            // Act
            var result = await repo.UpdateHall(update);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateHall_UpdatesHall_WhenFound()
        {
            // Arrange
            var repo = new HallRepository(_context);

            var update = new Hall
            {
                HallId = 1,
                HallNumber = 10,
                Capacity = 150
            };

            // Act
            var result = await repo.UpdateHall(update);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.HallId);
            Assert.Equal(10, result.HallNumber);
            Assert.Equal(150, result.Capacity);


            var fromDb = await repo.GetHallById(1);
            Assert.NotNull(fromDb);
            Assert.Equal(10, fromDb.HallNumber);
            Assert.Equal(150, fromDb.Capacity);
        }

        [Fact]
        public async Task DeleteHallAsync_ReturnsFalse_WhenNotFound()
        {
            // Arrange
            var repo = new HallRepository(_context);

            // Act
            var result = await repo.DeleteHallAsync(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteHallAsync_DeletesHall_WhenFound()
        {
            // Arrange
            var repo = new HallRepository(_context);

            // Act
            var result = await repo.DeleteHallAsync(2);

            // Assert
            Assert.True(result);

            var deleted = await repo.GetHallById(2);
            Assert.Null(deleted);

            var all = await repo.GetAllHalls();
            Assert.Equal(1, all.Count);
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SecureFlight.Api.Controllers;
using SecureFlight.Core.DataTransferObjects;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;
using SecureFlight.Core.Services;
using SecureFlight.Infrastructure.Repositories;
using Xunit;

namespace SecureFlight.Test
{
    public class AirportTests : IDisposable
    {
        private readonly SecureFlightDatabaseTestContext testingContext;

        public AirportTests()
        {
            testingContext = new SecureFlightDatabaseTestContext();
            testingContext.CreateDatabase();
        }

        public void Dispose()
        {
            testingContext.DisposeDatabase();
            testingContext.Dispose();
        }

        [Fact]
        public async Task Update_Succeeds()
        {
            //Arrange
            var repository = new BaseRepository<Airport>(testingContext);
            var airport = await repository.GetByIdAsync("JFK");
            airport.Name = "Updated Name";

            //Act
            repository.Update(airport);
            var affectedRows = await repository.SaveChangesAsync();

            //Assert
            Assert.Equal(1, affectedRows);
            testingContext.ResetTracking();
            var persisted = await testingContext.Airports.FindAsync("JFK");
            Assert.Equal("Updated Name", persisted!.Name);
        }

        [Fact]
        public async Task Put_ExistingAirport_PersistsChanges()
        {
            //Arrange
            var repository = new BaseRepository<Airport>(testingContext);
            var controller = new AirportsController(repository, new BaseService<Airport>(repository));
            var dto = new AirportDataTransferObject("JFK", "New Name", "New City", "New Country");

            //Act
            var result = await controller.Put("JFK", dto);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<AirportDataTransferObject>(okResult.Value);
            Assert.Equal(dto, returned);

            testingContext.ResetTracking();
            var persisted = await testingContext.Airports.FindAsync("JFK");
            Assert.NotNull(persisted);
            Assert.Equal("New Name", persisted.Name);
            Assert.Equal("New City", persisted.City);
            Assert.Equal("New Country", persisted.Country);
        }

        [Fact]
        public async Task Put_ExistingAirport_CallsSaveChanges()
        {
            //Arrange
            var airport = new Airport { Code = "JFK", Name = "Old", City = "Old", Country = "Old" };
            var mockRepository = new Mock<IRepository<Airport>>();
            mockRepository.Setup(r => r.GetByIdAsync("JFK")).ReturnsAsync(airport);
            mockRepository.Setup(r => r.Update(It.IsAny<Airport>())).Returns<Airport>(a => a);
            mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
            var controller = new AirportsController(mockRepository.Object, Mock.Of<IService<Airport>>());

            //Act
            await controller.Put("JFK", new AirportDataTransferObject("JFK", "New", "New", "New"));

            //Assert
            mockRepository.Verify(r => r.Update(airport), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Put_NonExistingAirport_ReturnsNotFoundAndDoesNotSave()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Airport>>();
            mockRepository.Setup(r => r.GetByIdAsync("XXX")).ReturnsAsync((Airport)null);
            var controller = new AirportsController(mockRepository.Object, Mock.Of<IService<Airport>>());

            //Act
            var result = await controller.Put("XXX", new AirportDataTransferObject("XXX", "N", "C", "C"));

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
            mockRepository.Verify(r => r.Update(It.IsAny<Airport>()), Times.Never);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
    }
}

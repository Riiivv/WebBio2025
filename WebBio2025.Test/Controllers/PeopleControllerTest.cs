using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBio2025.API.Controllers;
using WebBio2025.Application.DTOs;
using WebBio2025.Application.Interfaces;
using Xunit;

namespace WebBio2025.Test.Controllers
{
    public class PeopleControllerTest
    {
        private readonly IPersonService _service;
        private readonly PeopleController _controller;

        public PeopleControllerTest()
        {
            _service = A.Fake<IPersonService>();
            _controller = new PeopleController(_service);
        }
        [Fact]
        public async Task GetAllPersons_CallsService_ReturnsOk()
        {
            // Arrange
            var expected = new List<PersonDTOResponse>
            {
                new PersonDTOResponse { Id = 1, Name = "John", Mail = "john@test.com" },
                new PersonDTOResponse { Id = 2, Name = "Jane", Mail = "jane@test.com" }
            };

            A.CallTo(() => _service.GetAllPersons())
                .Returns(Task.FromResult<IEnumerable<PersonDTOResponse>>(expected));

            // Act
            var result = await _controller.GetAllPersons();

            // Assert - service called
            A.CallTo(() => _service.GetAllPersons()).MustHaveHappenedOnceExactly();

            // Assert - 200 OK + payload
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var payload = Assert.IsAssignableFrom<IEnumerable<PersonDTOResponse>>(ok.Value).ToList();

            Assert.Equal(2, payload.Count);
            Assert.Equal("John", payload[0].Name);
        }

        [Fact]
        public async Task GetPersonById_CallsService_ReturnOK()
        {
            // Arrange
            var id = 1;
            var expectedPerson = new PersonDTOResponse
            {
                Id = id,
                Name = "John",
                Mail = "testmail@hallo.com"
            };

            A.CallTo(() => _service.GetPersonById(id))
                .Returns(Task.FromResult<PersonDTOResponse>(expectedPerson));

            // Act
            var result = await _controller.GetPersonById(id);

            // Assert - service called
            A.CallTo(() => _service.GetPersonById(id)).MustHaveHappenedOnceExactly();

            // Assert - 200 OK + payload
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var payload = Assert.IsType<PersonDTOResponse>(okResult.Value);

            Assert.Equal(expectedPerson.Id, payload.Id);
            Assert.Equal(expectedPerson.Name, payload.Name);
            Assert.Equal(expectedPerson.Mail, payload.Mail);
        }

        [Fact]
        public async Task GetPersonById_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var id = 999;

            A.CallTo(() => _service.GetPersonById(id))
                .Returns(Task.FromResult<PersonDTOResponse>(null));

            // Act
            var result = await _controller.GetPersonById(id);

            // Assert
            A.CallTo(() => _service.GetPersonById(id)).MustHaveHappenedOnceExactly();
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePerson_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Act
            var result = await _controller.CreatePerson(null);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Request body cannot be empty.", bad.Value);

            // Service må ikke blive kaldt
            A.CallTo(() => _service.CreatePerson(A<PersonDTORequest>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task CreatePerson_ReturnsBadRequest_WhenServiceReturnsNull()
        {
            // Arrange
            var request = new PersonDTORequest
            {
                Id = 0,
                Name = "Mathias",
                Lastname = "Altenburgg",
                Mail = "mathias@test.com"
            };

            A.CallTo(() => _service.CreatePerson(request))
                .Returns(Task.FromResult<PersonDTOResponse>(null));

            // Act
            var result = await _controller.CreatePerson(request);

            // Assert
            A.CallTo(() => _service.CreatePerson(request)).MustHaveHappenedOnceExactly();

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Failed to create person.", bad.Value);
        }

        [Fact]
        public async Task CreatePerson_ReturnsCreatedAtAction_WhenCreated()
        {
            // Arrange
            var request = new PersonDTORequest
            {
                Id = 0,
                Name = "Mathias",
                Lastname = "Altenburgg",
                Mail = "mathias@test.com"
            };

            var created = new PersonDTOResponse
            {
                Id = 10,
                Name = "Mathias",
                Mail = "mathias@test.com"
            };

            A.CallTo(() => _service.CreatePerson(request))
                .Returns(Task.FromResult<PersonDTOResponse>(created));

            // Act
            var result = await _controller.CreatePerson(request);

            // Assert
            A.CallTo(() => _service.CreatePerson(request)).MustHaveHappenedOnceExactly();

            var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(PeopleController.GetPersonById), createdAt.ActionName);

            var payload = Assert.IsType<PersonDTOResponse>(createdAt.Value);
            Assert.Equal(10, payload.Id);
            Assert.Equal("Mathias", payload.Name);

            Assert.NotNull(createdAt.RouteValues);
            Assert.Equal(10, createdAt.RouteValues["id"]);
        }

        [Fact]
        public async Task UpdatePerson_ReturnsBadRequest_WhenRequestIsNull()
        {
            // Act
            var result = await _controller.UpdatePerson(1, null);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Request body cannot be empty.", bad.Value);

            A.CallTo(() => _service.UpdatePerson(A<PersonDTORequest>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdatePerson_ReturnsBadRequest_WhenRouteIdDoesNotMatchBodyId()
        {
            // Arrange
            var request = new PersonDTORequest
            {
                Id = 2,
                Name = "X",
                Lastname = "Y",
                Mail = "x@y.com"
            };

            // Act
            var result = await _controller.UpdatePerson(1, request);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Route ID does not match request body ID.", bad.Value);

            A.CallTo(() => _service.UpdatePerson(A<PersonDTORequest>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdatePerson_ReturnsNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var request = new PersonDTORequest
            {
                Id = 1,
                Name = "Updated",
                Lastname = "Lastname",
                Mail = "updated@test.com"
            };

            A.CallTo(() => _service.UpdatePerson(request))
                .Returns(Task.FromResult<PersonDTOResponse>(null));

            // Act
            var result = await _controller.UpdatePerson(1, request);

            // Assert
            A.CallTo(() => _service.UpdatePerson(request)).MustHaveHappenedOnceExactly();
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task UpdatePerson_ReturnsOk_WhenUpdated()
        {
            // Arrange
            var request = new PersonDTORequest
            {
                Id = 1,
                Name = "Updated",
                Lastname = "Lastname",
                Mail = "updated@test.com"
            };

            var updated = new PersonDTOResponse
            {
                Id = 1,
                Name = "Updated",
                Mail = "updated@test.com"
            };

            A.CallTo(() => _service.UpdatePerson(request))
                .Returns(Task.FromResult<PersonDTOResponse>(updated));

            // Act
            var result = await _controller.UpdatePerson(1, request);

            // Assert
            A.CallTo(() => _service.UpdatePerson(request)).MustHaveHappenedOnceExactly();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var payload = Assert.IsType<PersonDTOResponse>(ok.Value);

            Assert.Equal(1, payload.Id);
            Assert.Equal("Updated", payload.Name);
        }


        [Fact]
        public async Task DeletePerson_ReturnsNotFound_WhenServiceReturnsFalse()
        {
            // Arrange
            var id = 999;

            A.CallTo(() => _service.DeletePerson(id))
                .Returns(Task.FromResult(false));

            // Act
            var result = await _controller.DeletePerson(id);

            // Assert
            A.CallTo(() => _service.DeletePerson(id)).MustHaveHappenedOnceExactly();
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeletePerson_ReturnsNoContent_WhenDeleted()
        {
            // Arrange
            var id = 2;

            A.CallTo(() => _service.DeletePerson(id))
                .Returns(Task.FromResult(true));

            // Act
            var result = await _controller.DeletePerson(id);

            // Assert
            A.CallTo(() => _service.DeletePerson(id)).MustHaveHappenedOnceExactly();
            Assert.IsType<NoContentResult>(result);
        }
    }
}

using application.DTO;
using application.Interfaces.Properties;
using housebroker.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace housebroker.test
{
    public class PropertiesControllerTests
    {
        private readonly Mock<IPropertyService> _mockPropertyService;
        private readonly PropertiesController _controller;

        public PropertiesControllerTests()
        {
            _mockPropertyService = new Mock<IPropertyService>();
            _controller = new PropertiesController(_mockPropertyService.Object);
        }

        #region GetAll Tests

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            // Arrange
            var expectedProperties = new List<PropertyDto>
            {
                new PropertyDto { Id = 1, Location = "Test Location 1", Price = 100000, Type = "Land" },
                new PropertyDto { Id = 2, Location = "Test Location 2", Price = 200000, Type = "Building" }
            };
            _mockPropertyService.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedProperties);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualProperties = Assert.IsType<List<PropertyDto>>(okResult.Value);
            Assert.Equal(2, actualProperties.Count);
            Assert.Equal(expectedProperties[0].Id, actualProperties[0].Id);
        }

       

        #endregion

        #region GetById Tests

        [Fact]
        public async Task GetById_ReturnsOkResult()
        {
            // Arrange
            var propertyId = 1;
            var expectedProperty = new PropertyDto
            {
                Id = propertyId,
                Location = "Test Location",
                Price = 100000,
                Type = "Land"
            };
            _mockPropertyService.Setup(s => s.GetByIdAsync(propertyId)).ReturnsAsync(expectedProperty);

            // Act
            var result = await _controller.GetById(propertyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualProperty = Assert.IsType<PropertyDto>(okResult.Value);
            Assert.Equal(expectedProperty.Id, actualProperty.Id);
            Assert.Equal(expectedProperty.Location, actualProperty.Location);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = 999;
            _mockPropertyService.Setup(s => s.GetByIdAsync(propertyId)).ReturnsAsync((PropertyDto?)null);

            // Act
            var result = await _controller.GetById(propertyId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Property with ID {propertyId} not found.", notFoundResult.Value);
        }

        #endregion

        #region Create Tests

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WithValidDto()
        {
            // Arrange
            var createDto = new CreatePropertyDto
            {
                Location = "Test Location",
                Price = 100000,
                Type = "Land"
            };
            var createdProperty = new PropertyDto
            {
                Id = 1,
                Location = createDto.Location,
                Price = createDto.Price,
                Type = createDto.Type,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _mockPropertyService.Setup(s => s.CreateAsync(createDto)).ReturnsAsync(createdProperty);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetById), createdResult.ActionName);
            Assert.Equal(createdProperty.Id, createdResult.RouteValues!["id"]);
            var returnedProperty = Assert.IsType<PropertyDto>(createdResult.Value);
            Assert.Equal(createdProperty.Id, returnedProperty.Id);
        }


        #endregion

        #region Update Tests

        [Fact]
        public async Task Update_ReturnsOkResult_WithValidDto()
        {
            // Arrange
            var propertyId = 1;
            var updateDto = new UpdatePropertyDto
            {
                Id = propertyId,
                Location = "Updated Location",
                Price = 150000,
                Type = "Building"
            };
            var updatedProperty = new PropertyDto
            {
                Id = propertyId,
                Location = updateDto.Location,
                Price = updateDto.Price,
                Type = updateDto.Type,
                UpdatedAt = DateTime.UtcNow
            };
            _mockPropertyService.Setup(s => s.UpdateAsync(updateDto)).ReturnsAsync(updatedProperty);

            // Act
            var result = await _controller.Update(propertyId, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedProperty = Assert.IsType<PropertyDto>(okResult.Value);
            Assert.Equal(updatedProperty.Id, returnedProperty.Id);
            Assert.Equal(updatedProperty.Location, returnedProperty.Location);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = 999;
            var updateDto = new UpdatePropertyDto
            {
                Id = propertyId,
                Location = "Updated Location",
                Price = 150000,
                Type = "Building"
            };
            _mockPropertyService.Setup(s => s.UpdateAsync(updateDto)).ReturnsAsync((PropertyDto?)null);

            // Act
            var result = await _controller.Update(propertyId, updateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal($"Property with ID {propertyId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WithInvalidModelState()
        {
            // Arrange
            var propertyId = 1;
            var updateDto = new UpdatePropertyDto(); // Invalid - missing required fields
            _controller.ModelState.AddModelError("Location", "Location is required");

            // Act
            var result = await _controller.Update(propertyId, updateDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        #endregion

        #region Delete Tests

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenPropertyExists()
        {
            // Arrange
            var propertyId = 1;
            _mockPropertyService.Setup(s => s.DeleteAsync(propertyId)).ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(propertyId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenPropertyDoesNotExist()
        {
            // Arrange
            var propertyId = 999;
            _mockPropertyService.Setup(s => s.DeleteAsync(propertyId)).ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(propertyId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Property with ID {propertyId} not found.", notFoundResult.Value);
        }

        #endregion

    }
}

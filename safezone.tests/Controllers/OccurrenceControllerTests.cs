using Microsoft.AspNetCore.Mvc;
using Moq;
using safezone.application.DTOs.Occurence;
using safezone.application.Interfaces;
using safezone.Controllers;
using safezone.domain.Entities;
using safezone.domain.Enums;

namespace safezone.tests.Controllers
{
    public class OccurrenceControllerTests
    {
        private readonly Mock<IOccurenceRepository> _repoMock;
        private readonly OccurenceController _controller;

        public OccurrenceControllerTests()
        {
            _repoMock = new Mock<IOccurenceRepository>();
            _controller = new OccurenceController(_repoMock.Object);
        }

        [Fact]
        public async Task GetById_Existing_ReturnsOk()
        {
            var occurrence = new Occurrence { Id = 1 };
            _repoMock.Setup(r => r.GetOccurrenceByIdAsync(1)).ReturnsAsync(occurrence);

            var result = await _controller.GetById(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(occurrence, ok.Value);
        }

        [Fact]
        public async Task GetById_NotFound_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetOccurrenceByIdAsync(1)).ReturnsAsync((Occurrence)null);

            var result = await _controller.GetById(1);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_Valid_ReturnsCreatedAtAction()
        {
            var dto = new OccurenceDTO { Title = "Test", Description = "Desc", Latitude = 0, Longitude = 0, Type = TypeOccurence.Assault, Address = "Address", UserId = 1 };

            var result = await _controller.Create(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var occ = Assert.IsType<Occurrence>(created.Value);
            Assert.Equal("Test", occ.Title);
        }

        [Fact]
        public async Task Update_Existing_ReturnsNoContent()
        {
            var existing = new Occurrence { Id = 1 };
            var dto = new OccurenceDTO { Title = "Updated", Description = "Desc", Latitude = 1, Longitude = 1, Type = TypeOccurence.Assault, Address = "Address" };

            _repoMock.Setup(r => r.GetOccurrenceByIdAsync(1)).ReturnsAsync(existing);

            var result = await _controller.Update(1, dto);

            Assert.IsType<NoContentResult>(result);
            Assert.Equal("Updated", existing.Title);
        }

        [Fact]
        public async Task Update_NotFound_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetOccurrenceByIdAsync(1)).ReturnsAsync((Occurrence)null);

            var result = await _controller.Update(1, new OccurenceDTO());

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Delete_Existing_ReturnsNoContent()
        {
            var existing = new Occurrence { Id = 1 };
            _repoMock.Setup(r => r.GetOccurrenceByIdAsync(1)).ReturnsAsync(existing);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NotFound_ReturnsNotFound()
        {
            _repoMock.Setup(r => r.GetOccurrenceByIdAsync(1)).ReturnsAsync((Occurrence)null);

            var result = await _controller.Delete(1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsList()
        {
            var list = new List<Occurrence> { new Occurrence { Id = 1 } };
            _repoMock.Setup(r => r.GetAllOccurrencesAsync()).ReturnsAsync(list);

            var result = await _controller.GetAll();

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(list, ok.Value);
        }
    }
}

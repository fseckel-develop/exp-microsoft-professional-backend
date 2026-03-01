using FluentAssertions;
using LogiTrack.Api.Contracts.Inventory;
using LogiTrack.Api.Controllers;
using LogiTrack.Api.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LogiTrack.Api.Tests.Controllers;

public class InventoryControllerTests
{
    [Fact]
    public async Task GetAll_ShouldReturnOkWithItems()
    {
        var serviceMock = new Mock<IInventoryService>();
        serviceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<InventoryItemResponseDto>
        {
            new() { InventoryItemId = 1, Name = "Widget", QuantityInStock = 10, Location = "A1", Price = 5m }
        });

        var controller = new InventoryController(serviceMock.Object);

        var result = await controller.GetAll();

        var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var items = ok.Value.Should().BeAssignableTo<IEnumerable<InventoryItemResponseDto>>().Subject;

        items.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetById_WhenFound_ShouldReturnOk()
    {
        var serviceMock = new Mock<IInventoryService>();
        serviceMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(
            new InventoryItemResponseDto { InventoryItemId = 1, Name = "Widget", QuantityInStock = 10, Location = "A1", Price = 5m });

        var controller = new InventoryController(serviceMock.Object);

        var result = await controller.GetById(1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_WhenMissing_ShouldReturnNotFound()
    {
        var serviceMock = new Mock<IInventoryService>();
        serviceMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((InventoryItemResponseDto?)null);

        var controller = new InventoryController(serviceMock.Object);

        var result = await controller.GetById(1);

        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction()
    {
        var dto = new CreateInventoryItemDto
        {
            Name = "Widget",
            QuantityInStock = 10,
            Location = "A1",
            Price = 5m
        };

        var response = new InventoryItemResponseDto
        {
            InventoryItemId = 1,
            Name = "Widget",
            QuantityInStock = 10,
            Location = "A1",
            Price = 5m
        };

        var serviceMock = new Mock<IInventoryService>();
        serviceMock.Setup(x => x.CreateAsync(dto)).ReturnsAsync(response);

        var controller = new InventoryController(serviceMock.Object);

        var result = await controller.Create(dto);

        result.Result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Delete_WhenMissing_ShouldReturnNotFound()
    {
        var serviceMock = new Mock<IInventoryService>();
        serviceMock.Setup(x => x.DeleteAsync(1)).ReturnsAsync(false);

        var controller = new InventoryController(serviceMock.Object);

        var result = await controller.Delete(1);

        result.Should().BeOfType<NotFoundObjectResult>();
    }
}
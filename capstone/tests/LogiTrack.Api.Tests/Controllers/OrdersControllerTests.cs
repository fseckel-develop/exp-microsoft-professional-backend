using FluentAssertions;
using LogiTrack.Api.Contracts.Orders;
using LogiTrack.Api.Controllers;
using LogiTrack.Api.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LogiTrack.Api.Tests.Controllers;

public class OrdersControllerTests
{
    [Fact]
    public async Task GetById_WhenFound_ShouldReturnOk()
    {
        var serviceMock = new Mock<IOrderService>();
        serviceMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(
            new OrderResponseDto { OrderId = 1, CustomerName = "Acme" });

        var controller = new OrdersController(serviceMock.Object);

        var result = await controller.GetById(1);

        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetById_WhenMissing_ShouldReturnNotFound()
    {
        var serviceMock = new Mock<IOrderService>();
        serviceMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((OrderResponseDto?)null);

        var controller = new OrdersController(serviceMock.Object);

        var result = await controller.GetById(1);

        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Create_WhenSuccessful_ShouldReturnCreatedAtAction()
    {
        var dto = new CreateOrderDto
        {
            CustomerName = "Acme",
            OrderItems = new List<CreateOrderItemDto>()
        };

        var serviceMock = new Mock<IOrderService>();
        serviceMock.Setup(x => x.CreateAsync(dto)).ReturnsAsync(
            OrderServiceResult.Successful(
                new OrderResponseDto { OrderId = 1, CustomerName = "Acme" }));

        var controller = new OrdersController(serviceMock.Object);

        var result = await controller.Create(dto);

        result.Result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Create_WhenInsufficientStock_ShouldReturnBadRequest()
    {
        var dto = new CreateOrderDto
        {
            CustomerName = "Acme",
            OrderItems = new List<CreateOrderItemDto>()
        };

        var serviceMock = new Mock<IOrderService>();
        serviceMock.Setup(x => x.CreateAsync(dto)).ReturnsAsync(
            OrderServiceResult.Failure(OrderErrorCode.InsufficientStock, "Not enough stock."));

        var controller = new OrdersController(serviceMock.Object);

        var result = await controller.Create(dto);

        result.Result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task AddItemToOrder_WhenOrderMissing_ShouldReturnNotFound()
    {
        var dto = new CreateOrderItemDto
        {
            InventoryItemId = 1,
            QuantityOrdered = 2
        };

        var serviceMock = new Mock<IOrderService>();
        serviceMock.Setup(x => x.AddItemAsync(1, dto)).ReturnsAsync(
            OrderServiceResult.Failure(OrderErrorCode.OrderNotFound, "Order not found."));

        var controller = new OrdersController(serviceMock.Object);

        var result = await controller.AddItemToOrder(1, dto);

        result.Result.Should().BeOfType<NotFoundObjectResult>();
    }
}
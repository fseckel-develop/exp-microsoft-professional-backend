using LogiTrack.Api.Contracts.Orders;
using LogiTrack.Api.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LogiTrack.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all orders",
        Description = "Returns the complete list of orders."
    )]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{orderId:int}")]
    [SwaggerOperation(
        Summary = "Get order by ID",
        Description = "Returns a single order by its unique identifier."
    )]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<OrderResponseDto>> GetById(int orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);

        if (order is null)
            return NotFound($"Order {orderId} not found.");

        return Ok(order);
    }

    [HttpPost]
    [Authorize(Policy = "OrderWrite")]
    [SwaggerOperation(
        Summary = "Create a new order",
        Description = "Creates a new order and deducts the ordered quantities from inventory. Allowed for Admin and SalesStaff users."
    )]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<OrderResponseDto>> Create(CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _orderService.CreateAsync(dto);

        if (!result.Success)
            return MapFailure(result);

        return CreatedAtAction(
            nameof(GetById),
            new { orderId = result.Order!.OrderId },
            result.Order);
    }

    [HttpPost("{orderId:int}/items")]
    [Authorize(Policy = "OrderWrite")]
    [SwaggerOperation(
        Summary = "Add an item to an order",
        Description = "Adds a new inventory item to an existing order or increases quantity if the item already exists in the order."
    )]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<OrderResponseDto>> AddItemToOrder(int orderId, CreateOrderItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _orderService.AddItemAsync(orderId, dto);

        if (!result.Success)
            return MapFailure(result);

        return Ok(result.Order);
    }

    [HttpDelete("{orderId:int}/items/{inventoryItemId:int}")]
    [Authorize(Policy = "OrderWrite")]
    [SwaggerOperation(
        Summary = "Remove an item from an order",
        Description = "Removes an inventory item from an order and restores its quantity to stock."
    )]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<OrderResponseDto>> RemoveItemFromOrder(int orderId, int inventoryItemId)
    {
        var result = await _orderService.RemoveItemAsync(orderId, inventoryItemId);

        if (!result.Success)
            return MapFailure(result);

        return Ok(result.Order);
    }

    [HttpPatch("{orderId:int}")]
    [Authorize(Policy = "OrderWrite")]
    [SwaggerOperation(
        Summary = "Update order information",
        Description = "Updates editable order information such as the customer name."
    )]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<OrderResponseDto>> UpdateOrderInfo(int orderId, UpdateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _orderService.UpdateOrderInfoAsync(orderId, dto);

        if (!result.Success)
            return MapFailure(result);

        return Ok(result.Order);
    }

    [HttpPatch("{orderId:int}/items/{inventoryItemId:int}")]
    [Authorize(Policy = "OrderWrite")]
    [SwaggerOperation(
        Summary = "Adjust an order item quantity",
        Description = "Adjusts the quantity of an item already present in an order. Positive values reserve more stock, negative values restore stock."
    )]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<OrderResponseDto>> AdjustItemQuantity(
        int orderId,
        int inventoryItemId,
        [FromQuery] int quantityChange)
    {
        var result = await _orderService.AdjustItemQuantityAsync(orderId, inventoryItemId, quantityChange);

        if (!result.Success)
            return MapFailure(result);

        return Ok(result.Order);
    }

    [HttpDelete("{orderId:int}")]
    [Authorize(Policy = "AdminOnly")]
    [SwaggerOperation(
        Summary = "Delete an order",
        Description = "Deletes an order and restores all reserved item quantities to stock. This endpoint is restricted to Admin users."
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(int orderId)
    {
        var deleted = await _orderService.DeleteAsync(orderId);

        if (!deleted)
            return NotFound($"Order {orderId} not found.");

        return NoContent();
    }

        private ActionResult MapFailure(OrderServiceResult result)
    {
        return result.ErrorCode switch
        {
            OrderErrorCode.OrderNotFound => NotFound(result.Error),
            OrderErrorCode.InventoryItemNotFound => NotFound(result.Error),
            OrderErrorCode.OrderItemNotFound => NotFound(result.Error),
            OrderErrorCode.InsufficientStock => BadRequest(result.Error),
            OrderErrorCode.ValidationError => BadRequest(result.Error),
            _ => BadRequest(result.Error ?? "Order operation failed.")
        };
    }
}
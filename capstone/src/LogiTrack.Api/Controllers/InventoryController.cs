using LogiTrack.Api.Contracts.Inventory;
using LogiTrack.Api.Services.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LogiTrack.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Inventory")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all inventory items",
        Description = "Returns the complete list of inventory items."
    )]
    [ProducesResponseType(typeof(IEnumerable<InventoryItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<InventoryItemResponseDto>>> GetAll()
    {
        var items = await _inventoryService.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{itemId:int}")]
    [SwaggerOperation(
        Summary = "Get inventory item by ID",
        Description = "Returns a single inventory item by its unique identifier."
    )]
    [ProducesResponseType(typeof(InventoryItemResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<InventoryItemResponseDto>> GetById(int itemId)
    {
        var item = await _inventoryService.GetByIdAsync(itemId);
        if (item is null)
            return NotFound($"Inventory item {itemId} not found.");

        return Ok(item);
    }

    [HttpPost]
    [Authorize(Policy = "InventoryWrite")]
    [SwaggerOperation(
        Summary = "Create a new inventory item",
        Description = "Creates a new inventory item. Allowed for Admin and WarehouseStaff users."
    )]
    [ProducesResponseType(typeof(InventoryItemResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<InventoryItemResponseDto>> Create(CreateInventoryItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var created = await _inventoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { itemId = created.InventoryItemId }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{itemId:int}")]
    [Authorize(Policy = "InventoryWrite")]
    [SwaggerOperation(
        Summary = "Update an inventory item",
        Description = "Updates an existing inventory item by ID. Allowed for Admin and WarehouseStaff users."
    )]
    [ProducesResponseType(typeof(InventoryItemResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<InventoryItemResponseDto>> Update(int itemId, UpdateInventoryItemDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _inventoryService.UpdateAsync(itemId, dto);
            if (updated is null)
                return NotFound($"Inventory item {itemId} not found.");

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{itemId:int}")]
    [Authorize(Policy = "AdminOnly")]
    [SwaggerOperation(
        Summary = "Delete an inventory item",
        Description = "Deletes an inventory item by ID. This endpoint is restricted to Admin users."
    )]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Delete(int itemId)
    {
        var deleted = await _inventoryService.DeleteAsync(itemId);
        if (!deleted)
            return NotFound($"Inventory item {itemId} not found.");

        return NoContent();
    }
}
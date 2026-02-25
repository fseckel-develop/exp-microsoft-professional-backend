using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogiTrack.Api.Entities;
using LogiTrack.Api.DTOs;

namespace LogiTrack.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly LogiTrackContext _context;

    public InventoryController(LogiTrackContext context) => _context = context;

    // GET: /api/inventory
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseInventoryItemDto>>> GetAll()
    {
        var items = await _context.InventoryItems
            .AsNoTracking()
            .Select(item => new ResponseInventoryItemDto
            {
                InventoryItemId = item.ItemId,
                Name = item.Name,
                QuantityInStock = item.QuantityInStock,
                Location = item.Location,
                Price = item.Price
            })
            .ToListAsync();

        return Ok(items);
    }

    // GET: /api/inventory/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ResponseInventoryItemDto>> GetById(int id)
    {
        var item = await _context.InventoryItems
            .AsNoTracking()
            .Where(i => i.ItemId == id)
            .Select(i => new ResponseInventoryItemDto
            {
                InventoryItemId = i.ItemId,
                Name = i.Name,
                QuantityInStock = i.QuantityInStock,
                Location = i.Location,
                Price = i.Price
            })
            .FirstOrDefaultAsync();

        return item == null ? NotFound() : Ok(item);
    }

    // POST: /api/inventory
    [HttpPost]
    public async Task<ActionResult<ResponseInventoryItemDto>> Create(CreateInventoryItemDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var entity = new InventoryItem
        {
            Name = dto.Name,
            QuantityInStock = dto.QuantityInStock,
            Location = dto.Location,
            Price = dto.Price
        };

        _context.InventoryItems.Add(entity);
        await _context.SaveChangesAsync();

        var responseDto = new ResponseInventoryItemDto
        {
            InventoryItemId = entity.ItemId,
            Name = entity.Name,
            QuantityInStock = entity.QuantityInStock,
            Location = entity.Location,
            Price = entity.Price
        };

        return CreatedAtAction(nameof(GetById), new { id = entity.ItemId }, responseDto);
    }

    // PUT: /api/inventory/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ResponseInventoryItemDto>> Update(int id, UpdateInventoryItemDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existing = await _context.InventoryItems.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = dto.Name;
        existing.QuantityInStock = dto.QuantityInStock;
        existing.Price = dto.Price;
        existing.Location = dto.Location;

        await _context.SaveChangesAsync();

        return Ok(new ResponseInventoryItemDto
        {
            InventoryItemId = existing.ItemId,
            Name = existing.Name,
            QuantityInStock = existing.QuantityInStock,
            Location = existing.Location,
            Price = existing.Price
        });
    }

    // DELETE: /api/inventory/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.InventoryItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
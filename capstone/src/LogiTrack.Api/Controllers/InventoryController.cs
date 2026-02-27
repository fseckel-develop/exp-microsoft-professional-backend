using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

using LogiTrack.Api.Entities;
using LogiTrack.Api.DTOs;

namespace LogiTrack.Api.Controllers;

[ApiController][Authorize]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly LogiTrackContext _context;
    private readonly IMemoryCache _cache;
    private const string InventoryCacheKey = "inventory_all";

    private static readonly Expression<Func<InventoryItem, ResponseInventoryItemDto>> InventorySelector =
        item => new ResponseInventoryItemDto
        {
            InventoryItemId = item.ItemId,
            Name = item.Name,
            QuantityInStock = item.QuantityInStock,
            Location = item.Location,
            Price = item.Price
        };

    public InventoryController(LogiTrackContext context, IMemoryCache cache) 
    {
        _context = context;
        _cache = cache;
    }

    // GET: /api/inventory
    [HttpGet][Authorize]
    public async Task<ActionResult<IEnumerable<ResponseInventoryItemDto>>> GetAll()
    {
        if (!_cache.TryGetValue(InventoryCacheKey, out List<ResponseInventoryItemDto>? items))
        {
            items = await _context.InventoryItems
                .AsNoTracking()
                .Select(InventorySelector)
                .ToListAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            
            _cache.Set(InventoryCacheKey, items, cacheEntryOptions);
        }

        return Ok(items);
    }

    // GET: /api/inventory/{id}
    [HttpGet("{itemId:int}")][Authorize]
    public async Task<ActionResult<ResponseInventoryItemDto>> GetById(int itemId)
    {
        string cacheKey = $"inventory_{itemId}";
        if (!_cache.TryGetValue(cacheKey, out ResponseInventoryItemDto? item))
        {
            item = await _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.ItemId == itemId)
                .Select(InventorySelector)
                .FirstOrDefaultAsync();

            if (item != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, item, cacheEntryOptions);
            }
        }

        return item == null ? NotFound($"Inventory item {itemId} not found.") : Ok(item);
    }

    // POST: /api/inventory
    [HttpPost][Authorize(Policy = "InventoryWrite")]
    public async Task<ActionResult<ResponseInventoryItemDto>> Create(CreateInventoryItemDto dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var item = new InventoryItem
        {
            Name = dto.Name,
            QuantityInStock = dto.QuantityInStock,
            Location = dto.Location,
            Price = dto.Price
        };

        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync();

        InvalidateCache();

        var responseDto = new ResponseInventoryItemDto
        {
            InventoryItemId = item.ItemId,
            Name = item.Name,
            QuantityInStock = item.QuantityInStock,
            Location = item.Location,
            Price = item.Price
        };

        return CreatedAtAction(nameof(GetById), new { itemId = item.ItemId }, responseDto);
    }

    // PUT: /api/inventory/{id}
    [HttpPut("{itemId:int}")][Authorize(Policy = "InventoryWrite")]
    public async Task<ActionResult<ResponseInventoryItemDto>> Update(int itemId, UpdateInventoryItemDto dto)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);

        var existing = await _context.InventoryItems.FindAsync(itemId);
        if (existing == null) 
            return NotFound($"Inventory item {itemId} not found.");

        existing.Name = dto.Name;
        existing.QuantityInStock = dto.QuantityInStock;
        existing.Price = dto.Price;
        existing.Location = dto.Location;

        await _context.SaveChangesAsync();

        InvalidateCache(itemId);

        var responseDto = new ResponseInventoryItemDto
        {
            InventoryItemId = existing.ItemId,
            Name = existing.Name,
            QuantityInStock = existing.QuantityInStock,
            Location = existing.Location,
            Price = existing.Price
        };

        return Ok(responseDto);
    }

    // DELETE: /api/inventory/{id}
    [HttpDelete("{itemId:int}")][Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> Delete(int itemId)
    {
        var item = await _context.InventoryItems.FindAsync(itemId);
        if (item == null) 
            return NotFound($"Inventory item {itemId} not found.");

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();

        InvalidateCache(itemId);

        return NoContent();
    }

    private void InvalidateCache(int? itemId = null)
    {
        _cache.Remove(InventoryCacheKey);
        if (itemId.HasValue) _cache.Remove($"inventory_{itemId.Value}");
    }
}
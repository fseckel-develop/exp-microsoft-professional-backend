using LogiTrack.Api.Data;
using LogiTrack.Api.Contracts.Inventory;
using LogiTrack.Api.Contracts.Mapping;
using LogiTrack.Api.Models;
using LogiTrack.Api.Services.Caching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.Api.Services.Inventory;

public class InventoryService : IInventoryService
{
    private readonly LogiTrackDbContext _context;
    private readonly IMemoryCache _cache;

    public InventoryService(LogiTrackDbContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<List<InventoryItemResponseDto>> GetAllAsync()
    {
        if (!_cache.TryGetValue(CacheKeys.InventoryAll, out List<InventoryItemResponseDto>? items))
        {
            items = await _context.InventoryItems
                .AsNoTracking()
                .Select(i => i.ToResponseDto())
                .ToListAsync();

            _cache.Set(CacheKeys.InventoryAll, items,
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        }

        return items ?? new List<InventoryItemResponseDto>();
    }

    public async Task<InventoryItemResponseDto?> GetByIdAsync(int itemId)
    {
        var cacheKey = CacheKeys.InventoryById(itemId);

        if (!_cache.TryGetValue(cacheKey, out InventoryItemResponseDto? item))
        {
            var entity = await _context.InventoryItems.FindAsync(itemId);
            if (entity is null)
                return null;

            item = entity.ToResponseDto();

            _cache.Set(cacheKey, item,
                new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5)));
        }

        return item;
    }

    public async Task<InventoryItemResponseDto> CreateAsync(CreateInventoryItemDto dto)
    {
        var item = new InventoryItem();
        item.UpdateDetails(dto.Name, dto.QuantityInStock, dto.Location, dto.Price);

        _context.InventoryItems.Add(item);
        await _context.SaveChangesAsync();

        InvalidateCache();

        return item.ToResponseDto();
    }

    public async Task<InventoryItemResponseDto?> UpdateAsync(int itemId, UpdateInventoryItemDto dto)
    {
        var existing = await _context.InventoryItems.FindAsync(itemId);
        if (existing is null)
            return null;

        existing.UpdateDetails(dto.Name, dto.QuantityInStock, dto.Location, dto.Price);

        await _context.SaveChangesAsync();
        InvalidateCache(itemId);

        return existing.ToResponseDto();
    }

    public async Task<bool> DeleteAsync(int itemId)
    {
        var item = await _context.InventoryItems.FindAsync(itemId);
        if (item is null)
            return false;

        _context.InventoryItems.Remove(item);
        await _context.SaveChangesAsync();

        InvalidateCache(itemId);
        return true;
    }

    private void InvalidateCache(int? itemId = null)
    {
        _cache.Remove(CacheKeys.InventoryAll);
        if (itemId.HasValue)
            _cache.Remove(CacheKeys.InventoryById(itemId.Value));
    }
}
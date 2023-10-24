using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class RoomViewComponent : ViewComponent
{
    private readonly IRoom _room;
    private readonly IMemoryCache _cache;

    public RoomViewComponent(IRoom room, IMemoryCache cache)
    {
        _room = room;
        _cache = cache;
    }

    // Asynchronous method
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var theRoom = await _cache.GetOrCreateAsync("CategoriesCacheKey", async (cacheEntry) =>
        {
            // Set cache entry options (e.g., cache duration)
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Cache for 30 minutes

            // Retrieve Rooms from the database
            return await _room.GetRooms();
        });

        return View(theRoom);
    }
}

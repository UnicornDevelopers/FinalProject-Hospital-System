
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

public class DepartmentViewComponent : ViewComponent
{
    private readonly IDepartment _department;
    private readonly IMemoryCache _cache;

    public DepartmentViewComponent(IDepartment department, IMemoryCache cache)
    {
        _department = department;
        _cache = cache;
    }

    // Asynchronous method
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var theDepartment = await _cache.GetOrCreateAsync("CategoriesCacheKey", async (cacheEntry) =>
        {
            // Set cache entry options (e.g., cache duration)
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30); // Cache for 30 minutes

            // Retrieve Departments from the database
            return await _department.GetDepartments();
        });

        return View(theDepartment);
    }
}


using DotnetRestApiReference.Models;

namespace DotnetRestApiReference.Services;

// This is just a prototype in-memory service; we will later use a real database
internal static class RegionsService
{
    private static List<Region> _regions = new();
    private static int _nextId = 1;

    // Fake some startup data
    static RegionsService()
    {
        _regions.Add(new Region(_nextId++, "Eastern United States"));
        _regions.Add(new Region(_nextId++, "Central United States"));
        _regions.Add(new Region(_nextId++, "Western United States"));
    }

    /* ************************************************************
    // Private Methods
    * ************************************************************/
    private static bool CheckExistsByName(string name)
    {
        return _regions.Any(r => r.Name == name);
    }
    
    private static bool CheckExistsById(int id)
    {
        return _regions.Any(r => r.Id == id);
    }

    /* ************************************************************
    // Public Methods
    * ************************************************************/
    public static Region Create(Region region)
    {
        // Check if region name is already in use
        if (CheckExistsByName(region.Name))
        {
            // TODO: Exception type?
            throw new Exception("Region name must be unique");
        }

        // Create the region
        var newRegion = new Region(_nextId++, region.Name);
        _regions.Add(newRegion);
        return newRegion;
    }

    public static Region Delete(int id)
    {
        // Check if region exists
        if (!CheckExistsById(id))
        {
            throw new Exception("Region not found");
        }

        // Delete the region
        var region = _regions.First(r => r.Id == id);
        _regions.Remove(region);
        return region;
    }

    public static List<Region> GetAll()
    {
        return _regions;
    }

    public static Region? GetById(int id)
    {
        return _regions.FirstOrDefault(r => r.Id == id);
    }

    public static Region Update(Region region)
    {
        // Check if region exists
        if (!CheckExistsById(region.Id))
        {
            throw new Exception("Region not found");
        }

        // Check if region name is already in use
        if (CheckExistsByName(region.Name))
        {
            throw new Exception("Region name must be unique");
        }

        // Update the region
        var index = _regions.FindIndex(r => r.Id == region.Id);

        _regions[index] = region;
        return region;
    }
}
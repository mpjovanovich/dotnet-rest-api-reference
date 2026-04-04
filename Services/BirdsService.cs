using DotnetRestApiReference.Models;

namespace DotnetRestApiReference.Services;

// This is just a prototype in-memory service; we will later use a real database
internal static class BirdsService
{
    private static List<Bird> _birds = new();
    private static int _nextId = 1;

    // Fake some startup data
    static BirdsService()
    {

        _birds.Add(new Bird(_nextId++, "Eastern Bluebird", "Sialia sialis", new List<int> { 1 }));
    }

    /* ************************************************************
    // Private Methods
    * ************************************************************/
    private static bool CheckExistsById(int id)
    {
        return _birds.Any(r => r.Id == id);
    }

    private static bool CheckUniqueConstraints(Bird bird)
    {
        return !_birds.Any(r => r.CommonName == bird.CommonName) 
            && !_birds.Any(r => r.Species == bird.Species);
    }

    /* ************************************************************
    // Public Methods
    * ************************************************************/
    public static Bird Create(Bird bird)
    {
        // Check if bird unique constraints are met
        if (!CheckUniqueConstraints(bird))
        {
            throw new Exception("Bird unique constraints not met");
        }

        // Check for invalid region ids
        if (bird.RegionIds.Any(id => RegionsService.GetById(id) is null))
        {
            throw new Exception("Invalid region ids");
        }

        // Create the bird
        var newBird = new Bird(_nextId++, bird.CommonName, bird.Species, bird.RegionIds);
        _birds.Add(newBird);
        return newBird;
    }

    public static Bird Delete(int id)
    {
        // Check if bird exists
        if (!CheckExistsById(id))
        {
            throw new Exception("Bird not found");
        }

        // Delete the bird
        var bird = _birds.First(r => r.Id == id);
        _birds.Remove(bird);
        return bird;
    }

    public static List<Bird> GetAll()
    {
        return _birds;
    }

    public static Bird? GetById(int id)
    {
        return _birds.FirstOrDefault(r => r.Id == id);
    }

    public static Bird Update(Bird bird)
    {
        // Check if bird exists
        if (!CheckExistsById(bird.Id))
        {
            throw new Exception("Bird not found");
        }

        // Check if bird unique constraints are met
        if (!CheckUniqueConstraints(bird))
        {
            throw new Exception("Bird unique constraints not met");
        }

        // Check for invalid region ids
        if (bird.RegionIds.Any(id => RegionsService.GetById(id) is null))
        {
            throw new Exception("Invalid region ids");
        }

        // Update the bird
        var index = _birds.FindIndex(r => r.Id == bird.Id);

        _birds[index] = bird;
        return bird;
    }
}
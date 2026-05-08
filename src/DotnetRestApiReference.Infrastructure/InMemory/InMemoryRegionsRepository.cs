using DotnetRestApiReference.Domain.Exceptions;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Repositories;

namespace DotnetRestApiReference.Infrastructure.InMemory;

public class InMemoryRegionsRepository : IRegionsRepository
{
    private List<Region> _regions = new();
    private int _nextId = 1;

    /*
    PRIVATE HELPER METHODS
    */
    // These constraints are handled by the database in production, but must be
    // enforced here so service-level logic is tested against realistic invariants.
    private bool ExistsByName(string name) => _regions.Any(r => r.Name == name);
    private void EnforceUniqueConstraints(Region region, Region? existingRegion = null)
    {
        if (
            region.Name != existingRegion?.Name
            && ExistsByName(region.Name)
        )
            throw new ConflictException("Region name already exists");
    }

    /*
    PUBLIC API
    */
    public Region? GetById(int id) => _regions.FirstOrDefault(r => r.Id == id);
    public List<Region> GetAll() => _regions;
    public Region Add(Region region)
    {
        EnforceUniqueConstraints(region);

        region = region with { Id = _nextId++ };
        _regions.Add(region);
        return region;
    }
    public Region Update(Region region)
    {
        Region? existingRegion = GetById(region.Id);
        if (existingRegion is null)
            throw new NotFoundException("Region", region.Id);
        EnforceUniqueConstraints(region, existingRegion);

        // Update the List
        int index = _regions.FindIndex(r => r.Id == region.Id);
        _regions[index] = region;

        return region;
    }
    public void Delete(int id) => _regions.RemoveAll(r => r.Id == id);
}

using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Repositories;

namespace DotnetRestApiReference.Infrastructure.InMemory;

public class InMemoryRegionsRepository : IRegionsRepository
{
    private List<Region> _regions = new();
    private int _nextId = 1;

    public Region? GetById(int id) => _regions.FirstOrDefault(r => r.Id == id);
    public List<Region> GetAll() => _regions;
    public Region Add(Region region)
    {
        region = region with { Id = _nextId++ };
        _regions.Add(region);
        return region;
    }
    public Region Update(Region region)
    {
        int index = _regions.FindIndex(r => r.Id == region.Id);
        if (index >= 0)
            _regions[index] = region;
        else
            _regions.Add(region);
        return region;
    }
    public void Delete(int id) => _regions.RemoveAll(r => r.Id == id);
    public bool ExistsByName(string name) => _regions.Any(r => r.Name == name);
}

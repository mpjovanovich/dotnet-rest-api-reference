using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Repositories;

namespace DotnetRestApiReference.Infrastructure.InMemory;

public class InMemoryBirdsRepository : IBirdsRepository
{
    private List<Bird> _birds = new();
    private int _nextId = 1;

    public Bird? GetById(int id) => _birds.FirstOrDefault(b => b.Id == id);
    public List<Bird> GetAll() => _birds;
    public Bird Add(Bird bird)
    {
        bird = bird with { Id = _nextId++ };
        _birds.Add(bird);
        return bird;
    }
    public Bird Update(Bird bird)
    {
        _birds.Add(bird);
        return bird;
    }
    public void Delete(int id) => _birds.RemoveAll(b => b.Id == id);
    public bool ExistsByCommonName(string name) => _birds.Any(b => b.CommonName == name);
    public bool ExistsBySpecies(string species) => _birds.Any(b => b.Species == species);
    public bool AnyInRegion(int regionId) => _birds.Any(b => b.RegionIds.Contains(regionId));
}
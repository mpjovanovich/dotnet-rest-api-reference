using DotnetRestApiReference.Domain.Exceptions;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Repositories;

namespace DotnetRestApiReference.Infrastructure.InMemory;

public class InMemoryBirdsRepository( IRegionsRepository regions ) : IBirdsRepository
{
    private List<Bird> _birds = new();
    private int _nextId = 1;

    /*
    PRIVATE HELPER METHODS
    */
    private bool ExistsByCommonName(string name) => _birds.Any(b => b.CommonName == name);
    private bool ExistsBySpecies(string species) => _birds.Any(b => b.Species == species);
    private void EnforceUniqueConstraints(Bird bird, Bird? existingBird = null)
    {
        if( 
            bird.CommonName != existingBird?.CommonName
            && ExistsByCommonName(bird.CommonName)
        )
            throw new ConflictException("Bird common name already exists");

        if( 
            bird.Species != existingBird?.Species
            && ExistsBySpecies(bird.Species)
        )
            throw new ConflictException("Bird species already exists");
    }
    private void EnforceRegionReferences(Bird bird)
    {
        if( bird.RegionIds.Any(id => regions.GetById(id) is null))
            throw new NotFoundException("Region", bird.RegionIds);
    }

    /*
    PUBLIC API
    */
    public Bird? GetById(int id) => _birds.FirstOrDefault(b => b.Id == id);
    public List<Bird> GetAll() => _birds;
    public Bird Add(Bird bird)
    {
        EnforceUniqueConstraints(bird);
        EnforceRegionReferences(bird);

        bird = bird with { Id = _nextId++ };
        _birds.Add(bird);
        return bird;
    }
    public Bird Update(Bird bird)
    {
        Bird? existingBird = GetById(bird.Id);
        if (existingBird is null)
            throw new NotFoundException("Bird", bird.Id);
        EnforceUniqueConstraints(bird, existingBird);
        EnforceRegionReferences(bird);

        _birds.Add(bird);
        return bird;
    }
    public void Delete(int id) => _birds.RemoveAll(b => b.Id == id);
}
using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Services;

public sealed class BirdsService(
    IBirdsRepository birdsRepository,
    IRegionsRepository regionsRepository
) : IBirdsService
{
    // TODO: This will move to InMemoryBirdsRepository.cs within the Infrastructure project

    // private List<Bird> _birds = new();
    // private int _nextId = 1;

    // Fake some startup data
    // public BirdsService()
    // {
    //     _birds.Add(new Bird(_nextId++, "Eastern Bluebird", "Sialia sialis", new List<int> { 1 }));
    // }

    /* ************************************************************
    // Private Methods
    * ************************************************************/
    private bool CheckUniqueConstraints(Bird bird)
    {
        return !birdsRepository.ExistsByCommonName(bird.CommonName)
            && !birdsRepository.ExistsBySpecies(bird.Species);
    }

    /* ************************************************************
    // Public Methods
    * ************************************************************/
    public Bird Create(Bird bird)
    {
        // Check if bird unique constraints are met
        if (!CheckUniqueConstraints(bird))
        {
            throw new Exception("Bird unique constraints not met");
        }

        // Check for invalid region ids
        if( bird.RegionIds.Any(id => regionsRepository.GetById(id) is null))
        {
            throw new Exception("Invalid region ids");
        }

        // Create the bird
        Bird newBird = new Bird(0, bird.CommonName, bird.Species, bird.RegionIds);
        newBird = birdsRepository.Add(newBird);
        return newBird;
    }

    public Bird Delete(int id)
    {
        // Check if bird exists
        Bird? bird = birdsRepository.GetById(id);
        if (bird is null)
        {
            throw new Exception("Bird not found");
        }

        // Delete the bird
        birdsRepository.Delete(id);
        return bird;
    }

    public List<Bird> GetAll()
    {
        List<Bird> birds = birdsRepository.GetAll();
        return birds;
    }

    public Bird? GetById(int id)
    {
        Bird? bird = birdsRepository.GetById(id);
        return bird;
    }

    public Bird Update(Bird bird)
    {
        // Check if bird exists
        if (birdsRepository.GetById(bird.Id) is null)
        {
            throw new Exception("Bird not found");
        }

        // Check if bird unique constraints are met
        if (!CheckUniqueConstraints(bird))
        {
            throw new Exception("Bird unique constraints not met");
        }

        // Check for invalid region ids
        if (bird.RegionIds.Any(id => regionsRepository.GetById(id) is null))
        {
            throw new Exception("Invalid region ids");
        }

        // Update the bird
        bird = birdsRepository.Update(bird);
        return bird;
    }
}
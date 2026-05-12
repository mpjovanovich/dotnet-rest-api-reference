using DotnetRestApiReference.Domain.Exceptions;
using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Services;

public sealed class BirdsService(
    IBirdsRepository birdsRepository
) : IBirdsService
{
    /* ************************************************************
    // Public Methods
    * ************************************************************/
    public Bird Create(Bird bird)
    {
        // Create the bird
        Bird newBird = new Bird(0, bird.CommonName, bird.Species, bird.RegionIds, bird.ImageUrl);
        newBird = birdsRepository.Add(newBird);
        return newBird;
    }

    public Bird Delete(int id)
    {
        // Check if bird exists
        Bird? bird = birdsRepository.GetById(id);
        if (bird is null)
        {
            throw new NotFoundException("Bird", id);
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
        // Update the bird
        bird = birdsRepository.Update(bird);
        return bird;
    }
}
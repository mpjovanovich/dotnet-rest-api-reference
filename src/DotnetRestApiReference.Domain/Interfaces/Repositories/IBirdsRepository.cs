using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Interfaces.Repositories;

public interface IBirdsRepository
{
    Bird? GetById(int id);
    List<Bird> GetAll();
    Bird Add(Bird bird);
    Bird Update(Bird bird);
    void Delete(int id);
    bool ExistsByCommonName(string name);
    bool ExistsBySpecies(string species);
    bool AnyInRegion(int regionId);
}
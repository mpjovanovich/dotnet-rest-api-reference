using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Interfaces.Repositories;

public interface IBirdsRepository
{
    Bird Add(Bird bird);
    void Delete(int id);
    List<Bird> GetAll();
    Bird? GetById(int id);
    Bird Update(Bird bird);
}
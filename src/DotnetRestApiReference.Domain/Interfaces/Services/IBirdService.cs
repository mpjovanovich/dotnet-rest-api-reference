using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Interfaces.Services;

public interface IBirdsService
{
    Bird Create(Bird bird);
    Bird Delete(int id);
    List<Bird> GetAll();
    Bird? GetById(int id);
    Bird Update(Bird bird);
}
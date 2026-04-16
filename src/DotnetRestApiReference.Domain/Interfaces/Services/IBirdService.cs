namespace DotnetRestApiReference.Domain.Interfaces.Services;

using DotnetRestApiReference.Domain.Models;

public interface IBirdsService
{
    Bird Create(Bird bird);
    Bird Delete(int id);
    List<Bird> GetAll();
    Bird? GetById(int id);
    Bird Update(Bird bird);
}
using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Interfaces.Services;

public interface IRegionsService
{
    Region Create(Region region);
    Region Delete(int id);
    List<Region> GetAll();
    Region? GetById(int id);
    Region Update(Region region);
}


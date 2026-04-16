namespace DotnetRestApiReference.Domain.Interfaces.Services;

using DotnetRestApiReference.Domain.Models;

public interface IRegionsService
{
    Region Create(Region region);
    Region Delete(int id);
    List<Region> GetAll();
    Region? GetById(int id);
    Region Update(Region region);
}


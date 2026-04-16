using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Interfaces.Repositories;

public interface IRegionsRepository
{
    Region? GetById(int id);
    List<Region> GetAll();
    Region Add(Region region);
    Region Update(Region region);
    void Delete(int id);
    bool ExistsByName(string name);
}
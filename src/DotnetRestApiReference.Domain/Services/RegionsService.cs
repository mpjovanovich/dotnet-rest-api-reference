using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Services;

public sealed class RegionsService(
    IRegionsRepository regionsRepository
) : IRegionsService
{
    /* ************************************************************
    // Public Methods
    * ************************************************************/
    public Region Create(Region region)
    {
        // Create the region
        Region newRegion = new Region(0, region.Name);
        newRegion = regionsRepository.Add(newRegion);
        return newRegion;
    }

    public Region Delete(int id)
    {
        // // Check if region exists
        Region? region = regionsRepository.GetById(id);
        if (region is null)
        {
            throw new Exception("Region not found");
        }

        // Delete the region
        regionsRepository.Delete(id);
        return region;
    }

    public List<Region> GetAll()
    {
        List<Region> regions = regionsRepository.GetAll();
        return regions;
    }

    public Region? GetById(int id)
    {
        Region? region = regionsRepository.GetById(id);
        return region;
    }

    public Region Update(Region region)
    {
        // Update the region
        region = regionsRepository.Update(region);
        return region;
    }
}
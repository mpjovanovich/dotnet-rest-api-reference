using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Domain.Services;

public sealed class RegionsService(
    IRegionsRepository regionsRepository,
    IBirdsRepository birdsRepository
) : IRegionsService
{
    /* ************************************************************
    // Private Methods
    * ************************************************************/
    // private bool CheckUniqueConstraints(Region region)
    // {
    //     return !regionsRepository.ExistsByName(region.Name);
    // }

    /* ************************************************************
    // Public Methods
    * ************************************************************/
    public Region Create(Region region)
    {
        // // Check if region unique constraints are met
        // if (!CheckUniqueConstraints(region))
        // {
        //     throw new Exception("Region unique constraints not met");
        // }

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

        // // Check if region has birds
        // if (birdsRepository.AnyInRegion(id))
        // {
        //     throw new Exception("Region has birds");
        // }

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
        // // Check if region exists
        // if (regionsRepository.GetById(region.Id) is null)
        // {
        //     throw new Exception("Region not found");
        // }

        // // Check if region unique constraints are met
        // if (!CheckUniqueConstraints(region))
        // {
        //     throw new Exception("Region unique constraints not met");
        // }

        // Update the region
        region = regionsRepository.Update(region);
        return region;
    }
}
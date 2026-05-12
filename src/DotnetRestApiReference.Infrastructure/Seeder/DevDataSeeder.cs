using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Models;

namespace DotnetRestApiReference.Infrastructure.Seeder;

internal static class DevDataSeeder
{
    // This adds some dummy data to the data store for development.
    // Any IRepository implementation should work with this.
    // It is assumed that the schema is already in place.
    internal static void Seed(IRegionsRepository regionsRepo, IBirdsRepository birdsRepo)
    {
        Region us_east = regionsRepo.Add(new Region(0, "United States East"));
        birdsRepo.Add(new Bird(0, "Northern Cardinal", "Cardinalidae", [us_east.Id], "northern-cardinal.jpg"));
        birdsRepo.Add(new Bird(0, "Mourning Dove", "Columbidae", [us_east.Id], "mourning-dove.jpg"));
    }
}
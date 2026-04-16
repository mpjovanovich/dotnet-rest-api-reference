namespace DotnetRestApiReference.Models;

internal record Bird(int Id, string CommonName, string Species, List<int> RegionIds);
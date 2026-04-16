namespace DotnetRestApiReference.Domain.Models;

public record Bird(int Id, string CommonName, string Species, List<int> RegionIds);
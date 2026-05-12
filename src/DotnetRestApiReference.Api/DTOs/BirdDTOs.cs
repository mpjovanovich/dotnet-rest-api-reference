namespace DotnetRestApiReference.Api.DTOs;

// Requests
public record CreateBirdRequest(string CommonName, string Species, List<int> RegionIds, string ImageUrl);
public record UpdateBirdRequest(string CommonName, string Species, List<int> RegionIds, string ImageUrl);

// Responses
public record BirdResponse(int Id, string CommonName, string Species, List<int> RegionIds, string ImageUrl);
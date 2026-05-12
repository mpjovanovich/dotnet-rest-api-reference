namespace DotnetRestApiReference.Api.DTOs;

// Requests
internal record CreateBirdRequest(string CommonName, string Species, List<int> RegionIds, string ImageUrl);
internal record UpdateBirdRequest(string CommonName, string Species, List<int> RegionIds, string ImageUrl);

// Responses
internal record BirdResponse(int Id, string CommonName, string Species, List<int> RegionIds, string ImageUrl);
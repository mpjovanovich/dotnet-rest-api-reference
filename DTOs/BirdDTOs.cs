namespace DotnetRestApiReference.DTOs;

// Requests
internal record CreateBirdRequest(string CommonName, string Species, List<int> RegionIds);

// Responses
internal record BirdResponse(int Id, string CommonName, string Species, List<int> RegionIds);
namespace DotnetRestApiReference.DTOs;

// Requests
internal record CreateRegionRequest(string Name);

// Responses
internal record RegionResponse(int Id, string Name);
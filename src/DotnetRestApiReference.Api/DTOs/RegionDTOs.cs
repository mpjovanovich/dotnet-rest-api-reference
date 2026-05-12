namespace DotnetRestApiReference.Api.DTOs;

// Requests
public record CreateRegionRequest(string Name);
public record UpdateRegionRequest(string Name);

// Responses
public record RegionResponse(int Id, string Name);
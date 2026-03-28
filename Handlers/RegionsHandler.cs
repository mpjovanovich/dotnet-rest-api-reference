using Microsoft.AspNetCore.Http.HttpResults;
using DotnetRestApiReference.Models;
using DotnetRestApiReference.DTOs;

namespace DotnetRestApiReference.Handlers;

internal static class RegionsHandler
{
    private static List<Region> _regions = new();
    private static int _nextId = 1;

    // Fake some startup data
    static RegionsHandler()
    {
        _regions.Add(new Region(_nextId++, "Eastern United States"));
        _regions.Add(new Region(_nextId++, "Central United States"));
        _regions.Add(new Region(_nextId++, "Western United States"));
    }

    /* ************************************************************
    // HELPERS
    * ************************************************************/
    private static RegionResponse ToResponse(Region r) =>
        new (r.Id, r.Name);

    /* ************************************************************
    // HANDLERS
    * ************************************************************/
    internal static Ok<List<RegionResponse>> GetRegions() =>
        TypedResults.Ok(_regions.Select(ToResponse).ToList());

    internal static Results<Created<RegionResponse>, BadRequest<string>> CreateRegion(
        CreateRegionRequest request)
    {
        // Check for duplicate name
        if (_regions.Any(r => r.Name == request.Name))
        {
            return TypedResults.BadRequest("Region name must be unique");
        }

        var region = new Region(
            _nextId++,
            request.Name
        );
        _regions.Add(region);
        return TypedResults.Created($"/regions/{region.Id}", ToResponse(region));
    }
}
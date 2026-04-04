using Microsoft.AspNetCore.Http.HttpResults;
using DotnetRestApiReference.Models;
using DotnetRestApiReference.DTOs;
using DotnetRestApiReference.Services;

namespace DotnetRestApiReference.Endpoints;

internal static class RegionsEndpoint
{
    public static void MapRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("/regions", CreateRegion);
        app.MapGet("/regions", GetRegions);
        app.MapGet("/regions/{id}", GetRegion);
        app.MapPut("/regions/{id}", UpdateRegion);
        app.MapDelete("/regions/{id}", DeleteRegion);
    }

    private static RegionResponse ToResponse(Region r) =>
        new (r.Id, r.Name);

    private static Ok<List<RegionResponse>> GetRegions() =>
        TypedResults.Ok(RegionsService.GetAll().Select(ToResponse).ToList());

    private static Results<Ok<RegionResponse>, NotFound<string>> GetRegion(int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var region = RegionsService.GetById(id);
        if (region is null)
        {
            return TypedResults.NotFound("Region not found");
        }

        /*
        * Return HTTP Response
        */
        return TypedResults.Ok(ToResponse(region));
    }

    private static Results<Created<RegionResponse>, BadRequest<string>> CreateRegion(
        CreateRegionRequest request)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Showing a basic example of validation at this layer:
        // Check for empty required fields 
        if (string.IsNullOrEmpty(request.Name))
        {
            return TypedResults.BadRequest("Region name cannot be empty");
        }

        /*
        * Delegate to service layer
        */
        // Create the region
        var region = RegionsService.Create(new Region(0, request.Name));

        /*
        * Return HTTP Response
        */
        return TypedResults.Created($"/regions/{region.Id}", ToResponse(region));
    }

    internal static Ok<RegionResponse> UpdateRegion(
        int id,
        UpdateRegionRequest request)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var region = RegionsService.Update(new Region(id, request.Name));

        /*
        * Return HTTP Response
        */
        return TypedResults.Ok(ToResponse(region));
    }

    internal static Ok<RegionResponse> DeleteRegion(int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var region = RegionsService.Delete(id);

        /*
        * Return HTTP Response
        */
        // Whether or not to return the deleted region is an API design decision.
        // We'll go ahead and return it to confirm that the delete was successful.
        return TypedResults.Ok(ToResponse(region));
    }
}
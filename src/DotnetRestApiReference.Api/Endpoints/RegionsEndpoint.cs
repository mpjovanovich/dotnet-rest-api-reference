using Microsoft.AspNetCore.Http.HttpResults;
using DotnetRestApiReference.Api.DTOs;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Services;

namespace DotnetRestApiReference.Api.Endpoints;

internal static class RegionsEndpoint
{
    public static void MapRoutes(this IEndpointRouteBuilder app, IRegionsService regionsService)
    {
        app.MapPost("/regions", (CreateRegionRequest request) => CreateRegion(regionsService, request));
        app.MapGet("/regions", () => GetRegions(regionsService));
        app.MapGet("/regions/{id}", (int id) => GetRegion(regionsService, id));
        app.MapPut("/regions/{id}", (int id, UpdateRegionRequest request) => UpdateRegion(regionsService, id, request));
        app.MapDelete("/regions/{id}", (int id) => DeleteRegion(regionsService, id));
   
    }

    private static RegionResponse ToResponse(Region r) =>
        new (r.Id, r.Name);

    private static Ok<List<RegionResponse>> GetRegions(IRegionsService regionsService) =>
        TypedResults.Ok(regionsService.GetAll().Select(ToResponse).ToList());

    private static Results<Ok<RegionResponse>, NotFound<string>> GetRegion(IRegionsService regionsService, int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var region = regionsService.GetById(id);
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
        IRegionsService regionsService, CreateRegionRequest request)
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
        Region region = regionsService.Create(new Region(0, request.Name));

        /*
        * Return HTTP Response
        */
        return TypedResults.Created($"/regions/{region.Id}", ToResponse(region));
    }

    internal static Ok<RegionResponse> UpdateRegion(
        IRegionsService regionsService,
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
        Region region = regionsService.Update(new Region(id, request.Name));

        /*
        * Return HTTP Response
        */
        return TypedResults.Ok(ToResponse(region));
    }

    internal static Ok<RegionResponse> DeleteRegion(IRegionsService regionsService, int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        Region region = regionsService.Delete(id);

        /*
        * Return HTTP Response
        */
        // Whether or not to return the deleted region is an API design decision.
        // We'll go ahead and return it to confirm that the delete was successful.
        return TypedResults.Ok(ToResponse(region));
    }
}
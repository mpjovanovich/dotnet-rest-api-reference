using Microsoft.AspNetCore.Http.HttpResults;
using DotnetRestApiReference.Api.DTOs;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Services.Interfaces.Services;

namespace DotnetRestApiReference.Api.Endpoints;

internal static class BirdsEndpoint
{
    public static void MapRoutes(this IEndpointRouteBuilder app, IBirdsService birdsService)
    {
        app.MapPost("/birds", CreateBird(birdsService));
        app.MapGet("/birds", GetBirds(birdsService));
        app.MapGet("/birds/{id}", GetBird(birdsService));
        app.MapPut("/birds/{id}", UpdateBird(birdsService));
        app.MapDelete("/birds/{id}", DeleteBird(birdsService));
    }

    private static BirdResponse ToResponse(Bird r) =>
        new (r.Id, r.CommonName, r.Species, r.RegionIds);

    private static Ok<List<BirdResponse>> GetBirds(IBirdsService birdsService) =>
        TypedResults.Ok(birdsService.GetAll().Select(ToResponse).ToList());

    private static Results<Ok<BirdResponse>, NotFound<string>> GetBird(IBirdsService birdsService, int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var bird = birdsService.GetById(id);
        if (bird is null)
        {
            return TypedResults.NotFound("Bird not found");
        }

        /*
        * Return HTTP Response
        */
        return TypedResults.Ok(ToResponse(bird));
    }

    private static Results<Created<BirdResponse>, BadRequest<string>> CreateBird(
        IBirdsService birdsService, CreateBirdRequest request)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Showing a basic example of validation at this layer:
        // Check for empty required fields 
        if (string.IsNullOrEmpty(request.CommonName) || string.IsNullOrEmpty(request.Species))
        {
            return TypedResults.BadRequest("Bird common name and species cannot be empty");
        }

        /*
        * Delegate to service layer
        */
        // Create the bird
        var bird = birdsService.Create(new Bird(0, request.CommonName, request.Species, request.RegionIds));

        /*
        * Return HTTP Response
        */
        return TypedResults.Created($"/birds/{bird.Id}", ToResponse(bird));
    }

    internal static Ok<BirdResponse> UpdateBird(
        IBirdsService birdsService,
        int id,
        UpdateBirdRequest request)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var bird = birdsService.Update(new Bird(id, request.CommonName, request.Species, request.RegionIds));

        /*
        * Return HTTP Response
        */
        return TypedResults.Ok(ToResponse(bird));
    }

    internal static Ok<BirdResponse> DeleteBird(IBirdsService birdsService, int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var bird = birdsService.Delete(id);

        /*
        * Return HTTP Response
        */
        // Whether or not to return the deleted bird is an API design decision.
        // We'll go ahead and return it to confirm that the delete was successful.
        return TypedResults.Ok(ToResponse(bird));
    }
}
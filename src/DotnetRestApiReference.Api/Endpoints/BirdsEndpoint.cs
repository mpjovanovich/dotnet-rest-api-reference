using Microsoft.AspNetCore.Http.HttpResults;
using DotnetRestApiReference.Models;
using DotnetRestApiReference.DTOs;
using DotnetRestApiReference.Services;

namespace DotnetRestApiReference.Endpoints;

internal static class BirdsEndpoint
{
    public static void MapRoutes(this IEndpointRouteBuilder app)
    {
        app.MapPost("/birds", CreateBird);
        app.MapGet("/birds", GetBirds);
        app.MapGet("/birds/{id}", GetBird);
        app.MapPut("/birds/{id}", UpdateBird);
        app.MapDelete("/birds/{id}", DeleteBird);
    }

    private static BirdResponse ToResponse(Bird r) =>
        new (r.Id, r.CommonName, r.Species, r.RegionIds);

    private static Ok<List<BirdResponse>> GetBirds() =>
        TypedResults.Ok(BirdsService.GetAll().Select(ToResponse).ToList());

    private static Results<Ok<BirdResponse>, NotFound<string>> GetBird(int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var bird = BirdsService.GetById(id);
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
        CreateBirdRequest request)
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
        var bird = BirdsService.Create(new Bird(0, request.CommonName, request.Species, request.RegionIds));

        /*
        * Return HTTP Response
        */
        return TypedResults.Created($"/birds/{bird.Id}", ToResponse(bird));
    }

    internal static Ok<BirdResponse> UpdateBird(
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
        var bird = BirdsService.Update(new Bird(id, request.CommonName, request.Species, request.RegionIds));

        /*
        * Return HTTP Response
        */
        return TypedResults.Ok(ToResponse(bird));
    }

    internal static Ok<BirdResponse> DeleteBird(int id)
    {
        /*
        * Validate HTTP Request at this layer
        * Business logic validation will happen in the service layer
        */
        // Omitted for this project scope

        /*
        * Delegate to service layer
        */
        var bird = BirdsService.Delete(id);

        /*
        * Return HTTP Response
        */
        // Whether or not to return the deleted bird is an API design decision.
        // We'll go ahead and return it to confirm that the delete was successful.
        return TypedResults.Ok(ToResponse(bird));
    }
}
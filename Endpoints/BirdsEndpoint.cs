using Microsoft.AspNetCore.Http.HttpResults;
using DotnetRestApiReference.Models;
using DotnetRestApiReference.DTOs;

namespace DotnetRestApiReference.Endpoints;

internal static class BirdsEndpoint
{
    private static List<Bird> _birds = new();
    private static int _nextId = 1;

    static BirdsEndpoint()
    {
        _birds.Add(new Bird(_nextId++, "Eastern Bluebird", "Sialia sialis", new List<int> { 1 }));
    }

    /* ************************************************************
    // ROUTES
    * ************************************************************/
    public static void MapRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/birds", GetBirds);
        app.MapPost("/birds", CreateBird);
    }

    /* ************************************************************
    // HELPERS
    * ************************************************************/
    private static BirdResponse ToResponse(Bird b) =>
        new (b.Id, b.CommonName, b.Species, b.RegionIds);

    /* ************************************************************
    // HANDLERS
    * ************************************************************/

    private static Ok<List<BirdResponse>> GetBirds() =>
        TypedResults.Ok(_birds.Select(ToResponse).ToList());

    // private static Results<Created<BirdResponse>, BadRequest> CreateBird(CreateBirdRequest request)
    private static Created<BirdResponse> CreateBird(CreateBirdRequest request)
    {
        Console.WriteLine($"Creating bird: {request}");

        var bird = new Bird(
            _nextId++,
            request.CommonName,
            request.Species,
            request.RegionIds ?? new()
        );
        _birds.Add(bird);
        return TypedResults.Created($"/birds/{bird.Id}", ToResponse(bird));
    }
}
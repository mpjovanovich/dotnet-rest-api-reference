using Microsoft.AspNetCore.Http.HttpResults;
using KataOfTheDayPoc.Models;
using KataOfTheDayPoc.DTOs;

namespace KataOfTheDayPoc.Endpoints;

internal static class BirdsEndpoint
{
    private static List<Bird> _birds = new();
    private static int _nextId = 2; // 2 b/c of example data

    // Fake some startup data
    static BirdsEndpoint()
    {
        _birds.Add(new Bird(1, "Eastern Bluebird", "Sialia sialis", new List<int> { 1 }));
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

// curl http://localhost:5001/birds
    private static Ok<List<BirdResponse>> GetBirds() =>
        TypedResults.Ok(_birds.Select(ToResponse).ToList());

/*
curl -v -X POST http://localhost:5001/birds \
-H "Content-Type: application/json" \
-d '{"commonName":"American Robin","species":"Turdus migratorius","regionIds":[1]}'
*/
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
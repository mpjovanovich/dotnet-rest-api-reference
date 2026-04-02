using DotnetRestApiReference.Handlers;

namespace DotnetRestApiReference.Endpoints;

internal static class RegionsEndpoint
{
    public static void MapRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("/regions", RegionsHandler.GetRegions);
        app.MapPost("/regions", RegionsHandler.CreateRegion);
        app.MapPut("/regions/{id}", RegionsHandler.UpdateRegion);
        app.MapDelete("/regions/{id}", RegionsHandler.DeleteRegion);
    }
}
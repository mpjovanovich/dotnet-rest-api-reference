using DotnetRestApiReference.Domain.Services;
// using DotnetRestApiReference.Infrastructure.InMemory;
using DotnetRestApiReference.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
// Gives us basic error handling; all that's needed for this project scope.
builder.Services.AddProblemDetails();
var app = builder.Build();

// Composition root: build the object graph
// var regionsRepo = new InMemoryRegionsRepository();
// var birdsRepo   = new InMemoryBirdsRepository();
// var regionsService = new RegionsService(regionsRepo, birdsRepo);
// var birdsService   = new BirdsService(birdsRepo, regionsRepo);

// BirdsEndpoint.MapRoutes(app, birdsService);
// RegionsEndpoint.MapRoutes(app, regionsService);

app.Run("http://localhost:5001");
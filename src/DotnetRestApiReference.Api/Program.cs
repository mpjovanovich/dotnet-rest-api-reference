using DotnetRestApiReference.Domain.Services;
using DotnetRestApiReference.Infrastructure.InMemory;
using DotnetRestApiReference.Api.Endpoints;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Gives us basic error handling; all that's needed for this project scope.
builder.Services.AddProblemDetails();
var app = builder.Build();

// Composition root: build the object graph
IRegionsRepository regionsRepo = new InMemoryRegionsRepository();
IBirdsRepository birdsRepo   = new InMemoryBirdsRepository();
IRegionsService regionsService = new RegionsService(regionsRepo, birdsRepo);
IBirdsService birdsService   = new BirdsService(birdsRepo, regionsRepo);

BirdsEndpoint.MapRoutes(app, birdsService);
RegionsEndpoint.MapRoutes(app, regionsService);

app.Run("http://localhost:5001");
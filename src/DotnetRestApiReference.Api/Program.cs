using DotnetRestApiReference.Domain.Services;
using DotnetRestApiReference.Infrastructure.InMemory;
using DotnetRestApiReference.Api.Endpoints;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Interfaces.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
var app = builder.Build();

// Composition root: build the object graph
IRegionsRepository regionsRepo = new InMemoryRegionsRepository();
IBirdsRepository birdsRepo = new InMemoryBirdsRepository(regionsRepo);

IRegionsService regionsService = new RegionsService(regionsRepo, birdsRepo);
IBirdsService birdsService = new BirdsService(birdsRepo);

// Map endpoints now that services are built
BirdsEndpoint.MapRoutes(app, birdsService);
RegionsEndpoint.MapRoutes(app, regionsService);

app.Run("http://localhost:5001");

// Make Program visible to the integration tests so that they can instantiate it.
// Otherwise the Program class will be scoped to "internal".
public partial class Program { }
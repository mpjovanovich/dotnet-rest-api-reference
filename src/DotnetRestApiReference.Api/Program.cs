using DotnetRestApiReference.Api.Endpoints;
using DotnetRestApiReference.Api.Extensions;
using DotnetRestApiReference.Domain.Extensions;
using DotnetRestApiReference.Infrastructure.Extensions;

/*
USAGE:
build: dotnet build
test: dotnet test
run: ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/DotnetRestApiReference.Api
*/
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();

// This is what adds services and repositories to the DI container.
// In this branch we no longer manually build the object graph.
builder.Services.AddSQLiteInfrastructure();
// TODO: won't work yet - no DB schema created
// builder.Services.AddDevelopmentDataSeeding(builder.Environment);
builder.Services.AddDomain();

var app = builder.Build();

// Serve up some static images to make the API more interesting.
app.UseImageStorage();

// Map endpoints now that services are built
BirdsEndpoint.MapRoutes(app);
RegionsEndpoint.MapRoutes(app);

// Run the application
app.Run("http://localhost:5001");

// Make Program visible to the integration tests so that they can instantiate it.
// Otherwise the Program class will be scoped to "internal".
public partial class Program { }
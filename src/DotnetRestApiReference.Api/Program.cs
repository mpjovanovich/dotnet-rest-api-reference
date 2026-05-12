using DotnetRestApiReference.Domain.Services;
using DotnetRestApiReference.Infrastructure.SQLite;
using DotnetRestApiReference.Api.Endpoints;
using DotnetRestApiReference.Api.Extensions;
using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Domain.Models;

/*
USAGE:
build: dotnet build
test: dotnet test
run: ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/DotnetRestApiReference.Api
*/
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
var app = builder.Build();

// Serve up some static images to make the API more interesting.
app.UseImageStorage();

// Composition root: build the object graph
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString is null)
    throw new Exception("DefaultConnection connection string is not set");

// IBirdsRepository birdsRepo = new SQLiteBirdsRepository(builder.Configuration.GetConnectionString("DefaultConnection"));
IRegionsRepository regionsRepo = new SQLiteRegionsRepository(connectionString);

// // If this is development, seed the database with some data.
// if (app.Environment.IsDevelopment())
// {
//     Region us_east = regionsRepo.Add(new Region(0, "United States East"));
//     birdsRepo.Add(new Bird(0, "Northern Cardinal", "Cardinalidae", [us_east.Id], "northern-cardinal.jpg"));
//     birdsRepo.Add(new Bird(0, "Mourning Dove", "Columbidae", [us_east.Id], "mourning-dove.jpg"));
// }

// IBirdsService birdsService = new BirdsService(birdsRepo);
IRegionsService regionsService = new RegionsService(regionsRepo);


// Map endpoints now that services are built
// BirdsEndpoint.MapRoutes(app, birdsService);
RegionsEndpoint.MapRoutes(app, regionsService);

// Run the application
app.Run("http://localhost:5001");

// Make Program visible to the integration tests so that they can instantiate it.
// Otherwise the Program class will be scoped to "internal".
public partial class Program { }
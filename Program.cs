using Microsoft.AspNetCore.Mvc;
using DotnetRestApiReference.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Tells ASP.NET Core to use RFC 7807 Problem Details for errors when appropriate.
// Gives us some very basic error handling; all that's needed for this project scope.
builder.Services.AddProblemDetails();

var app = builder.Build();

BirdsEndpoint.MapRoutes(app);
RegionsEndpoint.MapRoutes(app);

app.Run("http://localhost:5001");

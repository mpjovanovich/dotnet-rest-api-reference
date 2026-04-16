using DotnetRestApiReference.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Gives us basic error handling; all that's needed for this project scope.
builder.Services.AddProblemDetails();

var app = builder.Build();

BirdsEndpoint.MapRoutes(app);
RegionsEndpoint.MapRoutes(app);

app.Run("http://localhost:5001");

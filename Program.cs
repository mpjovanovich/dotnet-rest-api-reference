using Microsoft.AspNetCore.Mvc;
using DotnetRestApiReference.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

BirdsEndpoint.MapRoutes(app);
RegionsEndpoint.MapRoutes(app);

app.Run("http://localhost:5001");

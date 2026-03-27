using Microsoft.AspNetCore.Mvc;
using KataOfTheDayPoc.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

BirdsEndpoint.MapRoutes(app);

app.Run("http://localhost:5001");

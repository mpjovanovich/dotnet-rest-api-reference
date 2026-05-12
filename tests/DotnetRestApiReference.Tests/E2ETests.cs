using DotnetRestApiReference.Api.DTOs;
using DotnetRestApiReference.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace DotnetRestApiReference.Tests;

public class E2ETests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public E2ETests(WebApplicationFactory<Program> factory)
    {
        // Tests will run with the ASPNETCORE_ENVIRONMENT set to Development by
        // default, so we override it so that we don't pollute the test
        // environment with the "sample" seed data.
        _client = factory
            .WithWebHostBuilder(builder => builder.UseEnvironment("Testing"))
            .CreateClient();
    }

    // This ended up being an end-to-end test rather than an integration test,
    // but that's fine for this project.
    [Fact]
    public async Task e2e_insert_region_insert_bird_verify_static_image()
    {
        // Insert a region into the database.
        var region = new Region(0, "United States East");
        var regionPostResponse = await _client.PostAsJsonAsync("/regions", region);
        regionPostResponse.EnsureSuccessStatusCode();
        var regionResponse = await regionPostResponse.Content.ReadFromJsonAsync<RegionResponse>();
        Assert.NotNull(regionResponse);

        // Insert a bird into the database. Image file northern-cardinal.jpg exists on disk;
        // use a unique name/species so we do not collide with Development seed data.
        var bird = new Bird(0, "Northern Cardinal", "Cardinalidae", [regionResponse.Id], "northern-cardinal.jpg");
        var birdPostResponse = await _client.PostAsJsonAsync("/birds", bird);
        birdPostResponse.EnsureSuccessStatusCode();
        var birdResponse = await birdPostResponse.Content.ReadFromJsonAsync<BirdResponse>();
        Assert.NotNull(birdResponse);

        // Get the image from the response
        var imageResponse = await _client.GetAsync(birdResponse.ImageUrl);
        imageResponse.EnsureSuccessStatusCode();
        Assert.Equal("image/jpeg", imageResponse.Content.Headers.ContentType?.MediaType);
    }
}
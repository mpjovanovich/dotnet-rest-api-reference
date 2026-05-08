using Microsoft.AspNetCore.Mvc.Testing;

public class BirdsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BirdsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetBirds_ReturnsOk()
    {
        // Very basic "sanity check" test to see if the endpoints are returning anything.
        // This is here to show scaffolding of an integration test.
        var response = await _client.GetAsync("/birds");

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
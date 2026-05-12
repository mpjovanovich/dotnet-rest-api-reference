using Microsoft.AspNetCore.Mvc.Testing;

public class BirdsEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BirdsEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task application_serves_static_images()
    {
        var response = await _client.GetAsync("/images/northern-cardinal.jpg");
            response.EnsureSuccessStatusCode();
        Assert.Equal("image/jpeg", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task basic_api_call_returns_200_with_json_content_type()
    {
        // Very basic "sanity check" test to see if the endpoints are returning anything.
        // This is here to show scaffolding of an integration test.
        var response = await _client.GetAsync("/birds");
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }
}
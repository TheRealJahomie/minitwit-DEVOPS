using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace minitwit.Api.Test;

public class TestWebApi : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public TestWebApi (CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var client = _factory.CreateClient();
        string url = "/api/twit/public/?page=0";

        // Act
        var reponse = await client.GetAsync(url);

        // Assert 
        reponse.EnsureSuccessStatusCode();
    }
}
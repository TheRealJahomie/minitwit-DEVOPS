using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace minitwit.Api.Test;

public class TestWebApi : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public TestWebApi (WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Test1()
    {
        // Arrange
        var client = _factory.CreateClient();
        string url = "/";

        // Act
        var reponse = await client.GetAsync(url);

        // Assert 
        reponse.EnsureSuccessStatusCode();
    }
}
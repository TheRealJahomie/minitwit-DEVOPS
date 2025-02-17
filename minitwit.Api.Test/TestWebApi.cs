using System.Net.Http.Headers;
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
        client.DefaultRequestHeaders.ExpectContinue = false; 
        client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", "your_test_jwt_token");
        string url = "/api/twit/hejkaj";

        // Act
        var reponse = await client.GetAsync(url);

        // Assert 
        reponse.EnsureSuccessStatusCode();
    }
}
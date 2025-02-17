using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using minitwit.Application.Services;
using minitwit.Controllers;
using minitwit.Infrastructure.Data;
using Xunit;

namespace minitwit.Api.Test;

public class TestWebApi : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly DbContextOptions<ApplicationDbContext> builder;
    private readonly SqliteConnection connection;
    private readonly ApplicationDbContext context;
    private readonly FollowController followController;
    private readonly TwitsController twitsController;
    private readonly UserController userController;

    public TestWebApi ()
    {
        connection = new SqliteConnection("Filename=memory;Mode=memory;");
        connection.Open();

        builder = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite(connection)
        .Options;

        context = new ApplicationDbContext(builder);
        context.Database.EnsureCreatedAsync();

        SeedDatabase();

        followController = new FollowController(new FollowService(context));
        twitsController = new TwitsController(new TwitsService(context));
        userController = new UserController(new UserSerivce(context));
    }

    public void SeedDatabase()
    {

    }

    [Fact]
    public async Task Test1()
    {
        
    }
}
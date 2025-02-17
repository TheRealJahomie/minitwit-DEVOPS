using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using minitwit.Application.Services;
using minitwit.Controllers;
using RegisterRequest = minitwit.Infrastructure.Dtos.Requests.RegisterRequest;
using Xunit;
using minitwit.Infrastructure.Data;
using FluentAssertions;
using minitwit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace minitwit.Api.Test;

public class TestWebApi : IDisposable
{
    private readonly DbContextOptions<ApplicationDbContext> builder;
    private readonly SqliteConnection connection;
    private readonly ApplicationDbContext context;
    private readonly FollowController followController;
    private readonly TwitsController twitsController;
    private readonly AuthController authController;

    public TestWebApi()
    {
        connection = new SqliteConnection("Filename=memory;Mode=memory;");
        connection.Open();

        builder = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlite(connection)
        .Options;

        context = new ApplicationDbContext(builder);
        context.Database.EnsureCreatedAsync();

        followController = new FollowController(new FollowService(context));
        twitsController = new TwitsController(new TwitsService(context));
        authController = new AuthController(new AuthService(context));
    }

    public void Dispose()
    {
        context.Database.EnsureDeletedAsync();
        context.Dispose();
    }

    [Theory]
    [InlineData("user1", "user1@example.com", "default", "default")]
    public async Task TestRegister(string username, string email, string password, string confirmPassword)
    {
        // Arrange
        var registerRequest = new RegisterRequest(
            username,
            email,
            password,
            confirmPassword
        );

        var userExpected = new User();
        userExpected.Id = 0;
        userExpected.Username = username;
        userExpected.Email = email;
        userExpected.PasswordHash = password;
        
        // Act
        await authController.Register(registerRequest);
        var userActual = await context.Users.Where(u => u!.Username == username).FirstOrDefaultAsync();

        // Assert
        userActual.Should().NotBeNull();
        userActual.Should().BeEquivalentTo(userExpected);
    }

    [Theory]
    [InlineData("", "@example.com", "default", "default", "You have to enter a username")]
    [InlineData("meh", "meh@example.com", "", "", "You have to enter a password")]
    [InlineData("meh", "meh@example.com", "x", "y", "The two passwords do not match")]
    [InlineData("meh", "broken", "foo", "foo", "You have to enter a valid email address")]
    public async Task TestRegisterThrowsException(string username, string email, string password, string confirmPassword, string errorMessage)
    {
        // Arrange
        var registerRequest = new RegisterRequest(
            username,
            email,
            password,
            confirmPassword
        );

        var userExpected = new User();
        userExpected.Id = 0;
        userExpected.Username = username;
        userExpected.Email = email;
        userExpected.PasswordHash = password;

        // Act
        Func<Task> act = async () => await authController.Register(registerRequest);


        // Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage(errorMessage);
    }

    [Fact]
    public async Task TestLogin()
    {

    }

    [Fact]
    public async Task TestLogout()
    {

    }

    [Fact]
    public async Task TestAddMessage()
    {

    }

    [Fact]
    public async Task TestTimeLines()
    {

    }
}
using backend.Controllers;
using backend.DTOs.Account;
using backend.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Tests.Integration;

namespace Tests.Integration
{

    public class ControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            using var scope = factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }


        [Fact]
        public async Task GetAccount_ShouldReturnNotFound_WhenAccountDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync("/api/Account/nonexistentuser/password123");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateSession_ShouldReturnOkResult_WithSessionId()
        {
            // Arrange
            var session = new UserSession { Username = "user1" };
            var content = new StringContent(JsonSerializer.Serialize(session), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Session/create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = responseContent.Split(":")[1].Trim().Trim('"');
            int a = 3;
            Assert.Empty(result);
        }


    }
}

using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Backend.api.dtos;
using Backend.api.controllers;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Backend.api.test
{
    public class UserControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); // This is the "client"
        }

        [Fact]
        public async Task CreateUser_ReturnsCreated()
        {
            var userDto = new { Username = "smoke", Password = "password", Email = "smoke@gmail.com", Phone = "1234567890" };

            // The client sends an HTTP POST request to the test server
            var response = await _client.PostAsJsonAsync("/api/users", userDto);

            Console.WriteLine(response.Content.ReadAsStringAsync());

            // Verify the response
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ReturnsDeleted()
        {
            
        }
    }
}

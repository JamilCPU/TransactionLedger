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
            var createdUser = await this.CreateUserHelper(_client);
            Console.WriteLine("blah");
            Console.WriteLine(createdUser);
        }

        [Fact]
        public async Task UpdateUser_ReturnsUpdated()
        {
            var userDto = new { Username = "smoke2", Password = "password2", Email = "smoke2@gmail.com", Phone = "1234567891" };

            String userId = "1";
            String endpoint = $"/api/users/${1}";
            var response = await _client.PostAsJsonAsync(endpoint, userDto);
            Console.WriteLine(response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Fact]
        public async Task DeleteUser_ReturnsDeleted()
        {
            String userId = "1";
            String endpoint = $"api/users/{userId}";
            Console.WriteLine(endpoint);
            var response = await _client.DeleteAsync(endpoint);

            Console.WriteLine(response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        public async Task<UserDto?> CreateUserHelper(HttpClient _client)
        {
            var userDto = new { Username = "smoke", Password = "password", Email = "smoke@gmail.com", Phone = "1234567890" };
            var response = await _client.PostAsJsonAsync("/api/users", userDto);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.Content);
        
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }


    }
}

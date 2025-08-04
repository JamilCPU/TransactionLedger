using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Backend.api.dtos;
using Backend.api.controllers;
using Backend.data.entities;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit.Abstractions;
using System.Text.Json;

namespace Backend.api.test
{
    public class UserControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public UserControllerIntegrationTest(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient(); // This is the "client"
        }

        [Fact]
        public async Task CreateUser_ReturnsCreated()
        {
            await this.CreateUserHelper(_client);
        }

        [Fact]
        public async Task UpdateUser_ReturnsUpdated()
        {
            UserEntity? user = await this.CreateUserHelper(_client);
            Assert.NotNull(user);
            var updatedUser = new { Username = "updatedUser", Password = "updatedPassword", Email = "updatedEmail@gmail.com", Phone = "0987654321" };
            String userId = user.Id.ToString();
            String endpoint = $"/api/users/{userId}";
            var response = await _client.PutAsJsonAsync(endpoint, updatedUser);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Fact]
        public async Task DeleteUser_ReturnsDeleted()
        {
            UserEntity? createdUser = await this.CreateUserHelper(_client);
            String userId = createdUser.Id.ToString();
            String endpoint = $"api/users/{userId}";
            var response = await _client.DeleteAsync(endpoint);

            Console.WriteLine(response.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetUser_ReturnsUser()
        {
            UserEntity? user = await this.CreateUserHelper(_client);

            String endpoint = $"api/users/{user.Id}";
            var response = await _client.GetAsync(endpoint);
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Fact]
        public async Task GetAllUsers_ReturnsUsers()
        {
            var response = await this.GetAllUsersHelper(_client);
            Console.WriteLine(JsonSerializer.Serialize(response));
        }

        public async Task<UserEntity?> CreateUserHelper(HttpClient _client)
        {

            var userDto = new { Username = "smoke", Password = "password", Email = "smoke@gmail.com", Phone = "1234567890" };
            var response = await _client.PostAsJsonAsync("/api/users/new", userDto);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            return await response.Content.ReadFromJsonAsync<UserEntity>();

        }

        public async void DeleteUserHelper(HttpClient _client, string userId)
        {
            String endpoint = $"api/users/{userId}";
            var response = await _client.DeleteAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task<List<UserEntity>> GetAllUsersHelper(HttpClient _client)
        {
            String endpoint = $"api/users/getAllUsers";
            var response = await _client.GetAsync(endpoint);
            List<UserEntity>? users = await response.Content.ReadFromJsonAsync<List<UserEntity>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (users == null) return [];
            return users;
        }


    }
}

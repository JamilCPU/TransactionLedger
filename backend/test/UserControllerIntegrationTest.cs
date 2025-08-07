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
    public class UserControllerIntegrationTest : BaseTestClass
    {
        public UserControllerIntegrationTest(WebApplicationFactory<Program> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task CreateUser_ReturnsCreated()
        {
            var user = await CreateTestUser();
            Assert.NotNull(user);
            Assert.NotEqual(0, user.Id);
        }

        [Fact]
        public async Task UpdateUser_ReturnsUpdated()
        {
            var user = await CreateTestUser();
            Assert.NotNull(user);

            var updatedUser = new { Username = "updatedUser", Password = "updatedPassword", Email = "updatedEmail@gmail.com", Phone = "0987654321" };
            var endpoint = $"/api/users/{user.Id}";
            var response = await _client.PutAsJsonAsync(endpoint, updatedUser);

            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ReturnsDeleted()
        {
            var user = await CreateTestUser();
            var endpoint = $"/api/users/{user.Id}";
            var response = await _client.DeleteAsync(endpoint);

            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUser_ReturnsUser()
        {
            var user = await CreateTestUser();
            var endpoint = $"/api/users/{user.Id}";
            var response = await _client.GetAsync(endpoint);

            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsUsers()
        {
            // Create a few test users
            await CreateTestUser("user1");
            await CreateTestUser("user2");
            await CreateTestUser("user3");

            var endpoint = "/api/users/getAllUsers";
            var response = await _client.GetAsync(endpoint);
            var users = await response.Content.ReadFromJsonAsync<List<UserEntity>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(users);
            Assert.True(users.Count >= 3); // At least our 3 test users
        }

        // Registration Tests
        [Fact]
        public async Task Register_WithValidData_ReturnsSuccess()
        {
            var userDto = new
            {
                Username = $"testuser_{Guid.NewGuid().ToString("N")[..8]}",
                Password = "securePassword123",
                Email = $"test{Guid.NewGuid().ToString("N")[..8]}@gmail.com",
                Phone = "1234567890"
            };

            var response = await _client.PostAsJsonAsync("/api/auth/register", userDto);
            var responseContent = await response.Content.ReadAsStringAsync();

            _output.WriteLine($"Registration response: {responseContent}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var createdUser = await response.Content.ReadFromJsonAsync<UserEntity>();
            Assert.NotNull(createdUser);
            Assert.Equal(userDto.Username, createdUser.Username);
            Assert.Equal(userDto.Email, createdUser.Email);
            Assert.Equal(userDto.Phone, createdUser.Phone);
            Assert.NotEqual(0, createdUser.Id);
        }

        // Login Tests
        [Fact]
        public async Task Login_WithValidCredentials_ReturnsSuccess()
        {
            // First register a user
            var username = $"testuser_{Guid.NewGuid().ToString("N")[..8]}";
            var password = "securePassword123";
            var userDto = new
            {
                Username = username,
                Password = password,
                Email = $"test{Guid.NewGuid().ToString("N")[..8]}@gmail.com",
                Phone = "1234567890"
            };

            var registerResponse = await _client.PostAsJsonAsync("/api/auth/register", userDto);
            Assert.Equal(HttpStatusCode.OK, registerResponse.StatusCode);

            // Then try to login with the same credentials
            var loginDto = new { Username = username, Password = password };
            var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            var responseContent = await loginResponse.Content.ReadAsStringAsync();

            _output.WriteLine($"Login response: {responseContent}");
            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        }


    }
}
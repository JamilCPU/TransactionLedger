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
    public class AccountControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public AccountControllerTest(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task CreateAccount_ReturnsCreated()
        {
            await this.CreateAccountHelper(_client);
        }

        [Fact]
        public async Task Deposit_ReturnsUpdated()
        {
            AccountEntity? account = await this.CreateAccountHelper(_client);
            Assert.NotNull(account);
            var depositRequest = new { Amount = 100.0m, UserId = 1, AccountType = AccountEntity.AccountTypeEnum.CHECKING };
            String accountId = account.Id.ToString();
            String endpoint = $"/api/accounts/{accountId}/deposit";
            var response = await _client.PostAsJsonAsync(endpoint, depositRequest);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Withdraw_ReturnsUpdated()
        {
            AccountEntity? account = await this.CreateAccountHelper(_client);
            Assert.NotNull(account);
            
            // First deposit some money
            var depositRequest = new { Amount = 200.0m, UserId = 1, AccountType = AccountEntity.AccountTypeEnum.CHECKING };
            String accountId = account.Id.ToString();
            await _client.PostAsJsonAsync($"/api/accounts/{accountId}/deposit", depositRequest);
            
            // Then withdraw
            var withdrawRequest = new { Amount = 50.0m, UserId = 1, AccountType = AccountEntity.AccountTypeEnum.CHECKING };
            var response = await _client.PostAsJsonAsync($"/api/accounts/{accountId}/withdraw", withdrawRequest);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Transfer_ReturnsSuccess()
        {
            AccountEntity? account1 = await this.CreateAccountHelper(_client);
            AccountEntity? account2 = await this.CreateAccountHelper(_client);
            Assert.NotNull(account1);
            Assert.NotNull(account2);
            
            // Deposit money to first account
            var depositRequest = new { Amount = 300.0m, UserId = 1, AccountType = AccountEntity.AccountTypeEnum.CHECKING };
            await _client.PostAsJsonAsync($"/api/accounts/{account1.Id}/deposit", depositRequest);
            
            // Transfer money
            var transferRequest = new { Amount = 100.0m, FromAccountId = account1.Id, ToAccountId = account2.Id };
            var response = await _client.PostAsJsonAsync("/api/accounts/transfer", transferRequest);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAccount_ReturnsAccount()
        {
            AccountEntity? account = await this.CreateAccountHelper(_client);
            String endpoint = $"api/accounts/{account.Id}";
            var response = await _client.GetAsync(endpoint);
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllAccounts_ReturnsAccounts()
        {
            var response = await this.GetAllAccountsHelper(_client);
            Console.WriteLine(JsonSerializer.Serialize(response));
        }

        [Fact]
        public async Task DeleteAccount_ReturnsDeleted()
        {
            AccountEntity? createdAccount = await this.CreateAccountHelper(_client);
            String accountId = createdAccount.Id.ToString();
            String endpoint = $"api/accounts/{accountId}";
            var response = await _client.DeleteAsync(endpoint);
            Console.WriteLine(response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task<AccountEntity?> CreateAccountHelper(HttpClient _client)
        {
            // First create a user
            var userDto = new { Username = "testuser", Password = "password", Email = "test@gmail.com", Phone = "1234567890" };
            var userResponse = await _client.PostAsJsonAsync("/api/users/new", userDto);
            userResponse.EnsureSuccessStatusCode();
            var user = await userResponse.Content.ReadFromJsonAsync<UserEntity>();
            
            // Then create an account for that user
            var accountDto = new { Amount = 100.0m, UserId = user.Id, AccountType = AccountEntity.AccountTypeEnum.CHECKING };
            var response = await _client.PostAsJsonAsync("/api/accounts/new", accountDto);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            return await response.Content.ReadFromJsonAsync<AccountEntity>();
        }

        public async void DeleteAccountHelper(HttpClient _client, string accountId)
        {
            String endpoint = $"api/accounts/{accountId}";
            var response = await _client.DeleteAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task<List<AccountEntity>> GetAllAccountsHelper(HttpClient _client)
        {
            String endpoint = $"api/accounts/getAllAccounts";
            var response = await _client.GetAsync(endpoint);
            List<AccountEntity>? accounts = await response.Content.ReadFromJsonAsync<List<AccountEntity>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (accounts == null) return [];
            return accounts;
        }
    }
}
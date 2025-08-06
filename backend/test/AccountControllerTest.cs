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
    public class AccountControllerTest : BaseTestClass
    {
        public AccountControllerTest(WebApplicationFactory<Program> factory, ITestOutputHelper output) 
            : base(factory, output)
        {
        }

        [Fact]
        public async Task CreateAccount_ReturnsCreated()
        {
            var account = await CreateTestAccount();
            Assert.NotNull(account);
            Assert.NotEqual(0, account.Id);
        }

        [Fact]
        public async Task Deposit_ReturnsUpdated()
        {
            var account = await CreateTestAccount();
            Assert.NotNull(account);
            
            var depositRequest = new { Amount = 100.0m };
            var endpoint = $"/api/accounts/{account.Id}/deposit";
            var response = await _client.PostAsJsonAsync(endpoint, depositRequest);
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Withdraw_ReturnsUpdated()
        {
            var account = await CreateTestAccount(initialAmount: 200.0m);
            Assert.NotNull(account);
            
            // First deposit some money
            var depositRequest = new { Amount = 200.0m };
            await _client.PostAsJsonAsync($"/api/accounts/{account.Id}/deposit", depositRequest);
            
            // Then withdraw
            var withdrawRequest = new { Amount = 50.0m };
            var response = await _client.PostAsJsonAsync($"/api/accounts/{account.Id}/withdraw", withdrawRequest);
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Transfer_ReturnsSuccess()
        {
            var account1 = await CreateTestAccount(initialAmount: 300.0m);
            var account2 = await CreateTestAccount(initialAmount: 100.0m);
            Assert.NotNull(account1);
            Assert.NotNull(account2);
            
            // Transfer money
            var transferRequest = new { Amount = 100.0m, FromAccountId = account1.Id, ToAccountId = account2.Id };
            var response = await _client.PostAsJsonAsync("/api/accounts/transfer", transferRequest);
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAccount_ReturnsAccount()
        {
            var account = await CreateTestAccount();
            var endpoint = $"/api/accounts/{account.Id}";
            var response = await _client.GetAsync(endpoint);
            
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllAccounts_ReturnsAccounts()
        {
            // Create a few test accounts
            await CreateTestAccount();
            await CreateTestAccount();
            await CreateTestAccount();
            
            var endpoint = "/api/accounts/getAllAccounts";
            var response = await _client.GetAsync(endpoint);
            var accounts = await response.Content.ReadFromJsonAsync<List<AccountEntity>>();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(accounts);
            Assert.True(accounts.Count >= 3); // At least our 3 test accounts
        }

        [Fact]
        public async Task DeleteAccount_ReturnsDeleted()
        {
            var account = await CreateTestAccount();
            var endpoint = $"/api/accounts/{account.Id}";
            var response = await _client.DeleteAsync(endpoint);
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
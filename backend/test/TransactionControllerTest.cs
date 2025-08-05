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
    public class TransactionControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;

        public TransactionControllerTest(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient();
            _output = output;
        }

        [Fact]
        public async Task CreateTransaction_ReturnsCreated()
        {
            await this.CreateTransactionHelper(_client);
        }

        [Fact]
        public async Task GetTransaction_ReturnsTransaction()
        {
            TransactionEntity? transaction = await this.CreateTransactionHelper(_client);
            String endpoint = $"api/transactions/{transaction.Id}";
            var response = await _client.GetAsync(endpoint);
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllTransactions_ReturnsTransactions()
        {
            var response = await this.GetAllTransactionsHelper(_client);
            Console.WriteLine(JsonSerializer.Serialize(response));
        }

        [Fact]
        public async Task GetTransactionsByAccountId_ReturnsTransactions()
        {
            TransactionEntity? transaction = await this.CreateTransactionHelper(_client);
            String endpoint = $"api/transactions/account/{transaction.Account.Id}/transactions";
            var response = await _client.GetAsync(endpoint);
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTransaction_ReturnsDeleted()
        {
            TransactionEntity? createdTransaction = await this.CreateTransactionHelper(_client);
            String transactionId = createdTransaction.Id.ToString();
            String endpoint = $"api/transactions/{transactionId}";
            var response = await _client.DeleteAsync(endpoint);
            Console.WriteLine(response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task<TransactionEntity?> CreateTransactionHelper(HttpClient _client)
        {
            // First create a user
            var userDto = new { Username = "testuser", Password = "password", Email = "test@gmail.com", Phone = "1234567890" };
            var userResponse = await _client.PostAsJsonAsync("/api/users/new", userDto);
            userResponse.EnsureSuccessStatusCode();
            var user = await userResponse.Content.ReadFromJsonAsync<UserEntity>();
            
            // Then create an account for that user
            var accountDto = new { Amount = 100.0m, UserId = user.Id, AccountType = AccountEntity.AccountTypeEnum.CHECKING };
            var accountResponse = await _client.PostAsJsonAsync("/api/accounts/new", accountDto);
            accountResponse.EnsureSuccessStatusCode();
            var account = await accountResponse.Content.ReadFromJsonAsync<AccountEntity>();
            
            // Then create a transaction for that account
            var transactionDto = new { Amount = 100.0m, AccountId = account.Id, TransactionType = TransactionEntity.TransactionTypeEnum.DEPOSIT };
            var response = await _client.PostAsJsonAsync("/api/transactions/new", transactionDto);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            return await response.Content.ReadFromJsonAsync<TransactionEntity>();
        }

        public async void DeleteTransactionHelper(HttpClient _client, string transactionId)
        {
            String endpoint = $"api/transactions/{transactionId}";
            var response = await _client.DeleteAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        public async Task<List<TransactionEntity>> GetAllTransactionsHelper(HttpClient _client)
        {
            String endpoint = $"api/transactions/getAllTransactions";
            var response = await _client.GetAsync(endpoint);
            List<TransactionEntity>? transactions = await response.Content.ReadFromJsonAsync<List<TransactionEntity>>();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            if (transactions == null) return [];
            return transactions;
        }
    }
}
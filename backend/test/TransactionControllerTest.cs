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
    public class TransactionControllerTest : BaseTestClass
    {
        public TransactionControllerTest(WebApplicationFactory<Program> factory, ITestOutputHelper output) 
            : base(factory, output)
        {
        }

        [Fact]
        public async Task CreateTransaction_ReturnsCreated()
        {
            var transaction = await CreateTestTransaction();
            Assert.NotNull(transaction);
            Assert.NotEqual(0, transaction.Id);
        }

        [Fact]
        public async Task GetTransaction_ReturnsTransaction()
        {
            var transaction = await CreateTestTransaction();
            var endpoint = $"/api/transactions/{transaction.Id}";
            var response = await _client.GetAsync(endpoint);
            
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllTransactions_ReturnsTransactions()
        {
            // Create a few test transactions
            await CreateTestTransaction();
            await CreateTestTransaction();
            await CreateTestTransaction();
            
            var endpoint = "/api/transactions/getAllTransactions";
            var response = await _client.GetAsync(endpoint);
            var transactions = await response.Content.ReadFromJsonAsync<List<TransactionEntity>>();
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(transactions);
            Assert.True(transactions.Count >= 3); // At least our 3 test transactions
        }

        [Fact]
        public async Task GetTransactionsByAccountId_ReturnsTransactions()
        {
            var transaction = await CreateTestTransaction();
            var endpoint = $"/api/transactions/account/{transaction.Account.Id}/transactions";
            var response = await _client.GetAsync(endpoint);
            
            var jsonString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteTransaction_ReturnsDeleted()
        {
            var transaction = await CreateTestTransaction();
            var endpoint = $"/api/transactions/{transaction.Id}";
            var response = await _client.DeleteAsync(endpoint);
            
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
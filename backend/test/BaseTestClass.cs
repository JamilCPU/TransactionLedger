using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Backend.data.entities;
using System.Net.Http.Json;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace Backend.api.test
{
    public abstract class BaseTestClass : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        protected readonly HttpClient _client;
        protected readonly ITestOutputHelper _output;
        protected readonly List<int> _createdUserIds = new List<int>();
        protected readonly List<int> _createdAccountIds = new List<int>();
        protected readonly List<int> _createdTransactionIds = new List<int>();

        protected BaseTestClass(WebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _client = factory.CreateClient();
            _output = output;
        }

        public void Dispose()
        {
            //CleanupTestData();
        }

        private void CleanupTestData()
        {
            try
            {
                // Clean up in reverse order to respect foreign key constraints
                foreach (var transactionId in _createdTransactionIds)
                {
                    try
                    {
                        _client.DeleteAsync($"/api/transactions/{transactionId}").Wait();
                    }
                    catch (Exception ex)
                    {
                        _output.WriteLine($"Failed to cleanup transaction {transactionId}: {ex.Message}");
                    }
                }

                foreach (var accountId in _createdAccountIds)
                {
                    try
                    {
                        _client.DeleteAsync($"/api/accounts/{accountId}").Wait();
                    }
                    catch (Exception ex)
                    {
                        _output.WriteLine($"Failed to cleanup account {accountId}: {ex.Message}");
                    }
                }

                foreach (var userId in _createdUserIds)
                {
                    try
                    {
                        _client.DeleteAsync($"/api/users/{userId}").Wait();
                    }
                    catch (Exception ex)
                    {
                        _output.WriteLine($"Failed to cleanup user {userId}: {ex.Message}");
                    }
                }

                _createdTransactionIds.Clear();
                _createdAccountIds.Clear();
                _createdUserIds.Clear();
            }
            catch (Exception ex)
            {
                _output.WriteLine($"Error during cleanup: {ex.Message}");
            }
        }

        protected async Task<UserEntity> CreateTestUser(string username = null)
        {
            var userDto = new
            {
                Username = username ?? $"testuser_{Guid.NewGuid().ToString("N")[..8]}",
                Password = "password",
                Email = $"test{Guid.NewGuid().ToString("N")[..8]}@gmail.com",
                Phone = "1234567890"
            };
            Console.WriteLine(userDto.Username);
            Console.WriteLine(userDto.Password);
            Console.WriteLine(userDto.Email);
            Console.WriteLine(userDto.Phone);
            var response = await _client.PostAsJsonAsync("/api/users/new", userDto);
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<UserEntity>();

            if (user != null)
            {
                _createdUserIds.Add(user.Id);
            }

            return user;
        }

        protected async Task<AccountEntity> CreateTestAccount(int? userId = null, decimal initialAmount = 100.0m)
        {
            var user = userId.HasValue ? null : await CreateTestUser();
            var actualUserId = userId ?? user.Id;

            var accountDto = new
            {
                Amount = initialAmount,
                UserId = actualUserId,
                AccountType = AccountEntity.AccountTypeEnum.CHECKING
            };

            var response = await _client.PostAsJsonAsync("/api/accounts/new", accountDto);
            response.EnsureSuccessStatusCode();
            var account = await response.Content.ReadFromJsonAsync<AccountEntity>();

            if (account != null)
            {
                _createdAccountIds.Add(account.Id);
            }

            return account;
        }

        protected async Task<TransactionEntity> CreateTestTransaction(int? accountId = null, decimal amount = 100.0m)
        {
            var account = accountId.HasValue ? null : await CreateTestAccount();
            var actualAccountId = accountId ?? account.Id;

            var transactionDto = new
            {
                Amount = amount,
                AccountId = actualAccountId,
                TransactionType = TransactionEntity.TransactionTypeEnum.DEPOSIT
            };

            var response = await _client.PostAsJsonAsync("/api/transactions/new", transactionDto);
            response.EnsureSuccessStatusCode();
            var transaction = await response.Content.ReadFromJsonAsync<TransactionEntity>();

            if (transaction != null)
            {
                _createdTransactionIds.Add(transaction.Id);
            }

            return transaction;
        }
    }
}
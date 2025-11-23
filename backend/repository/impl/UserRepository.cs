using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(BankContext context, IAccountRepository accountRepository, ILogger<UserRepository> logger)
        {
            _context = context;
            _accountRepository = accountRepository;
            _logger = logger;
        }
        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            _logger.LogDebug(
                "CreateUser - Username: {Username}, Email: {Email}",
                user.Username,
                user.Email
            );

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation(
                    "CreateUser - Success - UserId: {UserId}, Username: {Username}",
                    user.Id,
                    user.Username
                );

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "CreateUser - Database ERROR - Username: {Username}, Email: {Email}. Exception: {ExceptionType}",
                    user.Username,
                    user.Email,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<UserEntity> UpdateUser(int id, UserEntity user)
        {
            _logger.LogDebug("UpdateUser - UserId: {UserId}, Username: {Username}", id, user.Username);

            try
            {
                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    _logger.LogWarning("UpdateUser - User not found - UserId: {UserId}", id);
                    return null;
                }

                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Phone = user.Phone;
                existingUser.Email = user.Email;

                await _context.SaveChangesAsync();
                
                _logger.LogDebug("UpdateUser - Success - UserId: {UserId}, Username: {Username}", id, existingUser.Username);
                return existingUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "UpdateUser - Database ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            _logger.LogDebug("DeleteUser - UserId: {UserId}", id);

            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("DeleteUser - User not found - UserId: {UserId}", id);
                    return false;
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("DeleteUser - Success - UserId: {UserId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "DeleteUser - Database ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<UserEntity?> GetUserById(int id)
        {
            _logger.LogTrace("GetUserById - Querying database - UserId: {UserId}", id);
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                
                if (user != null)
                {
                    _logger.LogTrace("GetUserById - Found - UserId: {UserId}, Username: {Username}", id, user.Username);
                }
                
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetUserById - Database ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    id,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<List<UserEntity>?> GetAllUsers()
        {
            _logger.LogTrace("GetAllUsers - Querying database");
            
            try
            {
                List<UserEntity> userEntities = [];
                userEntities = await _context.Users.ToListAsync();
                
                _logger.LogTrace("GetAllUsers - Found {Count} users", userEntities.Count);
                return userEntities;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetAllUsers - Database ERROR. Exception: {ExceptionType}",
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            _logger.LogTrace("GetUserByUsername - Querying database - Username: {Username}", username);
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                
                if (user != null)
                {
                    var accounts = await _accountRepository.GetAllAccountsByUserId(user.Id);
                    user.Accounts = accounts;
                    _logger.LogTrace("GetUserByUsername - Found - UserId: {UserId}, Username: {Username}, AccountCount: {AccountCount}", 
                        user.Id, user.Username, accounts?.Count ?? 0);
                }
                
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "GetUserByUsername - Database ERROR - Username: {Username}. Exception: {ExceptionType}",
                    username,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<UserEntity?> Login(string username, string password)
        {
            _logger.LogTrace("Login - Querying database - Username: {Username}", username);
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                
                if (user != null)
                {
                    _logger.LogTrace("Login - Found user - Username: {Username}", username);
                }
                
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Login - Database ERROR - Username: {Username}. Exception: {ExceptionType}",
                    username,
                    ex.GetType().Name
                );
                throw;
            }
        }


        private readonly BankContext _context;
        private readonly IAccountRepository _accountRepository;
    }
}
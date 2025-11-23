using Backend.service.intrface;
using Backend.api.dtos;
using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.AspNetCore.Identity;

namespace Backend.service.impl
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHash = new PasswordHasher<string>();
            _logger = logger;
        }

        public UserService(IUserRepository userRepository, IJwtService jwtService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHash = new PasswordHasher<string>();
            _jwtService = jwtService;
            _logger = logger;
        }
        public async Task<UserEntity> CreateUser(UserDto userDto)
        {
            _logger.LogInformation(
                "CreateUser - Username: {Username}, Email: {Email}",
                userDto.Username,
                userDto.Email
            );

            try
            {
                validateUserDto();

                var existingUser = await _userRepository.GetUserByUsername(userDto.Username);
                if (existingUser != null)
                {
                    _logger.LogWarning("CreateUser - Username already taken - Username: {Username}", userDto.Username);
                    throw new Exception("This username is already taken.");
                }

                string hashedPassword = _passwordHash.HashPassword(userDto.Username, userDto.Password);
                UserEntity userEntity = new UserEntity(userDto.Username, hashedPassword, userDto.Email, userDto.Phone);

                var user = await _userRepository.CreateUser(userEntity);
                
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
                    "CreateUser - ERROR - Username: {Username}, Email: {Email}. Exception: {ExceptionType}",
                    userDto.Username,
                    userDto.Email,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            _logger.LogInformation("DeleteUser - UserId: {UserId}", userId);
            
            try
            {
                var result = await _userRepository.DeleteUser(userId);
                
                if (result)
                {
                    _logger.LogInformation("DeleteUser - Success - UserId: {UserId}", userId);
                }
                else
                {
                    _logger.LogWarning("DeleteUser - User not found - UserId: {UserId}", userId);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "DeleteUser - ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    userId,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<UserEntity> UpdateUser(int userId, UserDto userDto)
        {
            _logger.LogInformation(
                "UpdateUser - UserId: {UserId}, Username: {Username}",
                userId,
                userDto.Username
            );

            try
            {
                validateUserDto();
                UserEntity userEntity = new UserEntity(userDto.Username, userDto.Password, userDto.Email, userDto.Phone);
                
                var updatedUser = await _userRepository.UpdateUser(userId, userEntity);
                
                _logger.LogInformation(
                    "UpdateUser - Success - UserId: {UserId}, Username: {Username}",
                    userId,
                    updatedUser.Username
                );

                return updatedUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "UpdateUser - ERROR - UserId: {UserId}. Exception: {ExceptionType}",
                    userId,
                    ex.GetType().Name
                );
                throw;
            }
        }

        public async Task<UserEntity?> GetUserById(int userId)
        {
            _logger.LogDebug("GetUserById - UserId: {UserId}", userId);
            var user = await _userRepository.GetUserById(userId);
            
            if (user == null)
            {
                _logger.LogWarning("GetUserById - User not found - UserId: {UserId}", userId);
            }
            else
            {
                _logger.LogDebug("GetUserById - Found - UserId: {UserId}, Username: {Username}", userId, user.Username);
            }
            
            return user;
        }

        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            _logger.LogDebug("GetUserByUsername - Username: {Username}", username);
            var user = await _userRepository.GetUserByUsername(username);
            
            if (user == null)
            {
                _logger.LogWarning("GetUserByUsername - User not found - Username: {Username}", username);
            }
            else
            {
                _logger.LogDebug("GetUserByUsername - Found - UserId: {UserId}, Username: {Username}", user.Id, user.Username);
            }
            
            return user;
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            _logger.LogDebug("GetAllUsers - Request received");
            var users = await _userRepository.GetAllUsers();
            var count = users?.Count ?? 0;
            
            _logger.LogDebug("GetAllUsers - Retrieved {Count} users", count);
            return users ?? new List<UserEntity>();
        }

        public async Task<SuccessfulLoginDto> Login(string username, string password)
        {
            _logger.LogInformation("Login - Attempting login for Username: {Username}", username);

            try
            {
                var user = await _userRepository.GetUserByUsername(username);
                if (user == null)
                {
                    _logger.LogWarning("Login - User not found - Username: {Username}", username);
                    // Purposefully abstract the error messages regardless of if the user exists vs the password is incorrect
                    throw new Exception("Invalid username or password");
                }

                var result = _passwordHash.VerifyHashedPassword(username, user.Password, password);
                if (result == PasswordVerificationResult.Failed)
                {
                    _logger.LogWarning("Login - Invalid password - Username: {Username}", username);
                    throw new Exception("Invalid username or password");
                }

                var token = _jwtService.GenerateToken(user);
                
                _logger.LogInformation(
                    "Login - Success - Username: {Username}, UserId: {UserId}",
                    username,
                    user.Id
                );

                return new SuccessfulLoginDto("Login successful", token, username, password);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    "Login - Failed - Username: {Username}. Exception: {ExceptionType}",
                    username,
                    ex.GetType().Name
                );
                throw;
            }
        }

        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<string> _passwordHash;
        private readonly IJwtService _jwtService;

        private void validateUserDto()
        {
            _logger.LogDebug("validateUserDto - Validating user request");
        }
    }
}
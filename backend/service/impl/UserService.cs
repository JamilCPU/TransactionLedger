using Backend.service.intrface;
using Backend.api.dtos;
using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.AspNetCore.Identity;

namespace Backend.service.impl
{
    public class UserService : IUserService
    {
        public async Task<UserEntity> CreateUser(UserDto userDto)
        {
            Console.WriteLine("Service: Attemping to CREATE user: " + userDto.Username);
            validateUserDto();
            string hashedPassword = _passwordHash.HashPassword(userDto.Username, userDto.Password);
            UserEntity userEntity = new UserEntity(userDto.Username, hashedPassword, userDto.Email, userDto.Phone);

            return await _userRepository.CreateUser(userEntity);
        }

        public async Task<bool> DeleteUser(int userId)
        {
            Console.WriteLine("Service: Attempting to DELETE user: " + userId);
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<UserEntity> UpdateUser(int userId, UserDto userDto)
        {
            Console.WriteLine("Service: Attempting to UPDATE user: " + userId);
            UserEntity userEntity = new UserEntity(userDto.Username, userDto.Password, userDto.Email, userDto.Phone);
            validateUserDto();
            return await _userRepository.UpdateUser(userId, userEntity);
        }

        public async Task<UserEntity?> GetUserById(int userId)
        {
            Console.WriteLine("Service: Attempting to GET user: " + userId);
            return await _userRepository.GetUserById(userId);
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            Console.WriteLine("Service: Attempting to GET all users");
            return await _userRepository.GetAllUsers();
        }

        public async Task<SuccessfulLoginDto> Login(string username, string password)
        {

            var user = await _userRepository.GetUserByUsername(username);
            if (user == null){
                throw new Exception("Invalid username or password");
            }
                //Purposefully abstract the error messages regardless of if the user exists vs the password is incorrect

            var result = _passwordHash.VerifyHashedPassword(username, user.Password, password);
            if (result == PasswordVerificationResult.Failed){
                throw new Exception("Invalid username or password");
            }
            var token = _jwtService.GenerateToken(user);
            return new SuccessfulLoginDto("Login successful", token, username, password);
        }

        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<string> _passwordHash;
        private readonly IJwtService _jwtService;

        private void validateUserDto()
        {
            Console.WriteLine("Perform UserDto validations");
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHash = new PasswordHasher<string>();
        }

        public UserService(IUserRepository userRepository, IJwtService jwtService){
            _userRepository = userRepository;
            _passwordHash = new PasswordHasher<string>();
            _jwtService = jwtService;
        }
    }
}
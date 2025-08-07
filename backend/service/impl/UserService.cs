using Backend.service.intrface;
using Backend.api.dtos;
using Backend.repository.impl;
using Backend.data.entities;
using System.Threading.Tasks;
using Backend.repository.intrface;

namespace Backend.service.impl
{
    public class UserService : IUserService
    {
        public async Task<UserEntity> CreateUser(UserDto userDto)
        {
            Console.WriteLine("Service: Attemping to CREATE user: " + userDto.Username);
            validateUserDto();
            UserEntity userEntity = new UserEntity(userDto.Username, userDto.Password, userDto.Email, userDto.Phone);

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

        public async Task Login(UserDto userDto)
        {
            return await _userRepository.Login(userDto);
        }

        private readonly IUserRepository _userRepository;

        private void validateUserDto()
        {
            Console.WriteLine("Perform UserDto validations");
        }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
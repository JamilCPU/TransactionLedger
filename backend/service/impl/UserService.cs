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
        public async Task CreateUser(UserDto userDto)
        {
            Console.WriteLine("Service: Attemping to create user: " + userDto.Username);
            UserEntity userEntity = new UserEntity(userDto.Username, userDto.Password, userDto.Email, userDto.Phone);

            await _userRepository.CreateUser(userEntity);
        }
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
    }
}
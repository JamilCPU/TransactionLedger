using Backend.api.dtos;
using Backend.data.entities;

namespace Backend.service.intrface
{
    public interface IUserService
    {
        Task<UserEntity> CreateUser(UserDto userDto);

        Task<bool> DeleteUser(int userId);

        Task<UserEntity> UpdateUser(int userId, UserDto userDto);
        Task<UserEntity?> GetUserById(int userId);
        Task<List<UserEntity>> GetAllUsers();

        Task Login(string username, string password);
    }
}
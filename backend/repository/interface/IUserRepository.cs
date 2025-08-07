using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUser(UserEntity user);
        Task<UserEntity> UpdateUser(int id, UserEntity user);
        Task<bool> DeleteUser(int id);
        Task<UserEntity?> GetUserById(int id);
        Task<List<UserEntity>?> GetAllUsers();
        Task<UserEntity?> GetUserByUsername(string username);
        Task<UserEntity?> Login(string username, string password);
    }
}
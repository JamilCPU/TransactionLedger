using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface IUserRepository
    {
        Task CreateUser(UserEntity user);
        Task UpdateUser(UserEntity user);
        Task DeleteUser(int id);
        Task<UserEntity?> GetUserById(int id);
        Task<List<UserEntity>?> GetAllUsers();
    }
}
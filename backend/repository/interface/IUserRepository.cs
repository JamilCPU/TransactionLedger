using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface IUserRepository
    {
        Task createUser(UserEntity user);
        Task updateUser(UserEntity user);
        Task deleteUser(int id);
        Task<UserEntity?> getUserById(int id);
        Task<List<UserEntity>?> getAllUsers();
    }
}
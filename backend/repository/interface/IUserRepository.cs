using Backend.data.entities;
namespace Backend.repository.intrface{
    public interface IUserRepository
    {
        void createUser(UserEntity user);
        void updateUser(UserEntity user);
        void deleteUser(int id);
        UserEntity getUserById(int id);
        List<UserEntity> getAllUsers();
    }
}
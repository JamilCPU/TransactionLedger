using Backend.api.dtos;

namespace Backend.service.intrface{
    public interface IUserService{
        Task CreateUser(UserDto userDto);
    }
}
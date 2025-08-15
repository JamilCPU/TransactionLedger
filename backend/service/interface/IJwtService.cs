using Backend.data.entities;

public interface IJwtService{
    public string GenerateToken(UserEntity user);
}
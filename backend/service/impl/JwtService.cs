using Backend.service.intrface;
using Backend.data.entities;

namespace Backend.service.impl
{
    public class JwtService : IJwtService
    {
            private readonly JwtOptions _opt;

        public string GenerateToken(UserEntity user)
        {
            var now = DateTime.UtcNow;
            return "";
        }
    }
}
using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class UserRepository : IUserRepository
    {
        public async Task CreateUser(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(UserEntity user)
        {

        }

        public async Task DeleteUser(int id)
        {

        }

        public async Task<UserEntity?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserEntity>?> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        private readonly BankContext _context;

        public UserRepository(BankContext context)
        {
            _context = context;
}
    }
}
using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class UserRepository : IUserRepository
    {
        private readonly BankContext _context;
        public async Task createUser(UserEntity user)
        {

        }

        public async Task updateUser(UserEntity user)
        {

        }

        public async Task deleteUser(int id)
        {

        }
    
        public async Task<UserEntity?> getUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserEntity>?> getAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
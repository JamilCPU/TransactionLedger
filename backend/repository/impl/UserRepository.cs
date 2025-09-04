using Backend.data.entities;
using Backend.repository.intrface;
using Microsoft.EntityFrameworkCore;
using Backend.data;
namespace Backend.repository.impl
{
    public class UserRepository : IUserRepository
    {
        public async Task<UserEntity> CreateUser(UserEntity user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Console.WriteLine(user.ToString());//debug line
            return user;
        }

        public async Task<UserEntity> UpdateUser(int id, UserEntity user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) return null;

            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Phone = user.Phone;
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();
            return existingUser;

        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserEntity?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<UserEntity>?> GetAllUsers()
        {
            List<UserEntity> userEntities = [];
            userEntities = await _context.Users.ToListAsync();
            return userEntities;
        }

        public async Task<UserEntity?> GetUserByUsername(string username)
        {
            return await _context.Users
            .Where(u => u.Username == username)
            .Select(u => new UserEntity(u.Username, u.Password, u.Email, u.Phone, u.Accounts))
            .FirstOrDefaultAsync();
        }

        public async Task<UserEntity?> Login(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }


        private readonly BankContext _context;

        public UserRepository(BankContext context)
        {
            _context = context;
}
    }
}
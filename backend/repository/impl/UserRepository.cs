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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            var accounts = await _accountRepository.GetAllAccountsByUserId(user.Id);
            user.Accounts = accounts;
            return user;
        }

        public async Task<UserEntity?> Login(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }


        private readonly BankContext _context;
        private readonly IAccountRepository _accountRepository;
        public UserRepository(BankContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }
    }
}
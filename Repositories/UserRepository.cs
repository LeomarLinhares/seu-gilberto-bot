using Microsoft.EntityFrameworkCore;
using SeuGilbertoBot.Data;
using SeuGilbertoBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeuGilbertoBot.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BotDbContext _context;

        public UserRepository(BotDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<int> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}

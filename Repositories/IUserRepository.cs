using SeuGilbertoBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeuGilbertoBot.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}

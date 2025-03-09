using SeuGilbertoBot.Models;
using SeuGilbertoBot.Repositories;
using System.Threading.Tasks;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<string> RegisterUser(User user)
    {
        try
        {
            int changedItems = await _userRepository.AddUserAsync(user);
            string response = $@"
Nome: {user.FirstName} {user.LastName}
ID: {user.Id}
Username: {user.Username}
    ";
            return response;
        }
        catch
        {
            return $"Deu ruim para cadastrar";
        }
    }
}

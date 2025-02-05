using SeuGilbertoBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISeasonRepository
{
    Task<Season?> GetCurrentSeasonAsync();
    Task<IEnumerable<Season>> GetAllSeasonsAsync();
}

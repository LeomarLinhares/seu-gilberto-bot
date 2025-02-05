using Microsoft.EntityFrameworkCore;
using SeuGilbertoBot.Data;
using SeuGilbertoBot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SeasonRepository : ISeasonRepository
{
    private readonly BotDbContext _context;

    public SeasonRepository(BotDbContext context)
    {
        _context = context;
    }

    public async Task<Season?> GetCurrentSeasonAsync()
    {
        return await _context.Seasons
            .OrderByDescending(s => s.StartDate)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Season>> GetAllSeasonsAsync()
    {
        return await _context.Seasons.ToListAsync();
    }
}

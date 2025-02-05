using SeuGilbertoBot.Models;
using System.Threading.Tasks;

public class SeasonService
{
    private readonly ISeasonRepository _seasonRepository;

    public SeasonService(ISeasonRepository seasonRepository)
    {
        _seasonRepository = seasonRepository;
    }

    public async Task<string> GetCurrentSeasonInfo()
    {
        var season = await _seasonRepository.GetCurrentSeasonAsync();
        if (season == null)
        {
            return "⚠️ Nenhuma temporada encontrada.";
        }

        return $"🏆 Temporada Atual: {season.Year}\n📅 Iniciada em: {season.StartDate:dd/MM/yyyy}";
    }
}

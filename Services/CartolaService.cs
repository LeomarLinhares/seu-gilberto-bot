using SeuGilbertoBot.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeuGilbertoBot.Services
{
    public class CartolaService
    {
        private readonly HttpClient _httpClient;

        public CartolaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartolaStatusResponseDTO> GetStatus()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "SeuGilbertoBot");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            string url = "https://api.cartola.globo.com/mercado/status";

            try
            {
                var response = await _httpClient.GetStringAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    PropertyNameCaseInsensitive = true
                };

                return JsonSerializer.Deserialize<CartolaStatusResponseDTO>(response, options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter status: {ex.Message}");
                return null;
            }
        }
    }
}

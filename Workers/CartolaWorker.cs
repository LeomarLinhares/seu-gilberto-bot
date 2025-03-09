using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SeuGilbertoBot.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeuGilbertoBot.Workers
{
    public class CartolaWorker : BackgroundService
    {
        private readonly ILogger<CartolaWorker> _logger;
        private readonly CartolaService _cartolaService;
        private readonly TimeSpan _intervalo = TimeSpan.FromMinutes(1);

        public CartolaWorker(ILogger<CartolaWorker> logger, CartolaService cartolaService)
        {
            _logger = logger;
            _cartolaService = cartolaService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return;
            //_logger.LogInformation("CartolaWorker iniciado.");

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    try
            //    {
            //        var statusAtual = await _cartolaService.ObterStatusAsync();

            //        _logger.LogInformation($"Status do mercado: {statusAtual}");

            //        await Task.Delay(_intervalo, stoppingToken);
            //    }
            //    catch (Exception ex)
            //    {
            //        _logger.LogError(ex, "Erro ao verificar status do mercado.");
            //    }
            //}

            //_logger.LogInformation("CartolaWorker finalizado.");
        }
    }
}

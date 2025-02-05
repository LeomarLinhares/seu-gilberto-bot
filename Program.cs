using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeuGilbertoBot.Configurations;
using SeuGilbertoBot.Repositories;
using Telegram.Bot;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // 🔹 Carregar configuração do `appsettings.json`
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var appSettings = new AppSettings();
        configuration.Bind(appSettings);

        if (appSettings == null)
        {
            Console.WriteLine("Não foi possível obter o arquivo de configuração");
            return;
        }

        // 🔹 Obtém a string de conexão para SQLite
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // 🔹 Configurar injeção de dependências
        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton(appSettings);
                services.AddAppServices(connectionString); // Agora usa SQLite!
                services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(appSettings.TelegramBot.Token));

                services.AddScoped<DeepSeekService>(provider =>
                    new DeepSeekService(appSettings.DeepSeek.ApiKey, appSettings.DeepSeek.ApiUrl));

                services.AddScoped<BotCommandService>();
                services.AddScoped<BotService>();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var botService = serviceProvider.GetRequiredService<BotService>();
        botService.StartBot();

        Console.WriteLine("Digite o comando (/msg mensagem) ou 'sair' para encerrar:");

        while (true)
        {
            var input = Console.ReadLine();

            if (input?.ToLower() == "/sair")
                break;

            if (input.StartsWith("/msg"))
            {
                var parts = input.Split(' ', 2);

                if (parts.Length < 2)
                {
                    Console.WriteLine("Comando inválido. Use: /msg mensagem");
                    continue;
                }

                var message = parts[1];

                if (!string.IsNullOrWhiteSpace(message))
                {
                    var botClient = serviceProvider.GetRequiredService<ITelegramBotClient>();
                    await botClient.SendMessage(appSettings.TelegramBot.TargetGroup, message);
                    Console.WriteLine("Mensagem enviada!");
                }
                else
                {
                    Console.WriteLine("Mensagem inválida.");
                }
            }
            else
            {
                Console.WriteLine("Comando não reconhecido. Use: /msg mensagem");
            }
        }

        Console.WriteLine("Encerrando bot...");
    }
}

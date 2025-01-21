using Microsoft.Extensions.Configuration;
using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        var settings = configuration.GetSection("TelegramBot").Get<TelegramBotSettings>();

        if (settings == null)
        {
            Console.WriteLine("Não foi possível obter o arquivo de configuração");
            return;
        }

        var botClient = BotClientFactory.CreateBotClient(settings.Token);
        var commandService = new BotCommandService();
        var botService = new BotService(botClient, commandService);
        botService.StartBot();

        Console.WriteLine("Digite o comando (/msg mensagem) ou 'sair' para encerrar:");

        while (true)
        {
            // Lê o comando diretamente do console
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

                {
                    var message = parts[1];

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        await botClient.SendMessage(settings.TargetGroup, message);
                        Console.WriteLine("Mensagem enviada!");
                    }
                    else
                    {
                        Console.WriteLine("Mensagem inválida.");
                    }
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

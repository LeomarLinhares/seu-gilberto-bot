using Telegram.Bot;

class Program
{
    static async Task Main(string[] args)
    {
        var botClient = BotClientFactory.CreateBotClient();
        var commandService = new BotCommandService();
        var botService = new BotService(botClient, commandService);
        botService.StartBot();

        Console.WriteLine("Digite o comando (/msg [id-do-chat] mensagem) ou 'sair' para encerrar:");

        while (true)
        {
            // Lê o comando diretamente do console
            var input = Console.ReadLine();

            if (input?.ToLower() == "/sair")
                break;

            if (input.StartsWith("/msg"))
            {
                // Divide o comando em partes
                var parts = input.Split(' ', 3); // Divide em no máximo 3 partes: comando, id, mensagem

                if (parts.Length < 3)
                {
                    Console.WriteLine("Comando inválido. Use: /msg [id-do-chat] mensagem");
                    continue;
                }

                // Tenta converter o id-do-chat
                if (long.TryParse(parts[1], out long chatId))
                {
                    var message = parts[2]; // A mensagem é a terceira parte

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        await botClient.SendMessage(chatId, message);
                        Console.WriteLine("Mensagem enviada!");
                    }
                    else
                    {
                        Console.WriteLine("Mensagem inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("ID do chat inválido. Use um número válido.");
                }
            }
            else
            {
                Console.WriteLine("Comando não reconhecido. Use: /msg [id-do-chat] mensagem");
            }
        }

        Console.WriteLine("Encerrando bot...");
    }
}

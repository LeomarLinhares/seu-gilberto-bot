using System.Collections.Concurrent;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

public class BotCommandService
{
    private readonly DeepSeekService _deepSeekService;
    private static readonly ConcurrentDictionary<long, List<Message>> GroupMessages = new();

    public BotCommandService(DeepSeekService deepSeekService)
    {
        _deepSeekService = deepSeekService;
    }
    public void HandleMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var messageText = message.Text;
        var chatId = message.Chat.Id;

        if (string.IsNullOrWhiteSpace(messageText))
            return;

        if (messageText.StartsWith("/"))
        {
            var command = messageText.Split(' ')[0];
            if (command.Contains("@"))
            {
                command = command.Split("@")[0];
            }

            switch (command.ToLower())
            {
                case "/start":
                    botClient.SendMessage(chatId, "Bem-vindo ao bot!", cancellationToken: cancellationToken);
                    break;

                case "/rodadaInfo":
                    botClient.SendMessage(chatId, "__ROUND_INFO__", cancellationToken: cancellationToken);
                    break;

                case "/help":
                    botClient.SendMessage(chatId, "Comandos disponíveis:\n/start\n/help", cancellationToken: cancellationToken);
                    break;

                case "/cadastrar":
                    botClient.SendMessage(chatId, $"Usuário: {message.From.Username}, Id: {message.From.Id}", cancellationToken: cancellationToken);
                    break;

                default:
                    botClient.SendMessage(chatId, "Comando não reconhecido.", cancellationToken: cancellationToken);
                    break;
            }
        }
        else
        {
            ProcessRegularMessage(botClient, message, cancellationToken);
        }
    }

    private async void ProcessRegularMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        var messageText = message.Text;

        if (messageText.EndsWith("ão", StringComparison.OrdinalIgnoreCase))
        {
            botClient.SendMessage(chatId, "MEU PAU NA SUA MÃO", cancellationToken: cancellationToken);
        }

        //var response = await _deepSeekService.GetResponseAsync(messageText);
        //await botClient.SendMessage(chatId, response, cancellationToken: cancellationToken);
    }


    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Erro: {exception.Message}");
        return Task.CompletedTask;
    }

}

using Telegram.Bot;
using Telegram.Bot.Types;

public class BotCommandService
{
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

                case "/help":
                    botClient.SendMessage(chatId, "Comandos disponíveis:\n/start\n/help", cancellationToken: cancellationToken);
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

    private void ProcessRegularMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var chatId = message.Chat.Id;
        var messageText = message.Text;

        botClient.SendMessage(chatId, $"Você disse: {messageText}", cancellationToken: cancellationToken);

        if (messageText.Contains("Olá", StringComparison.OrdinalIgnoreCase))
        {
            botClient.SendMessage(chatId, "Oi! Como posso ajudar?", cancellationToken: cancellationToken);
        }
        else if (messageText.Contains("obrigado", StringComparison.OrdinalIgnoreCase))
        {
            botClient.SendMessage(chatId, "De nada! Estou aqui para ajudar.", cancellationToken: cancellationToken);
        }
    }


    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Erro: {exception.Message}");
        return Task.CompletedTask;
    }
}

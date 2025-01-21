using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

public class BotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly BotCommandService _commandService;

    public BotService(ITelegramBotClient botClient, BotCommandService commandService)
    {
        _botClient = botClient;
        _commandService = commandService;
    }

    public void StartBot()
    {
        var cancellationToken = new CancellationTokenSource().Token;

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            cancellationToken: cancellationToken
        );

        Console.ReadLine();
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is { } message)
        {
            if (message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group ||
            message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Supergroup)
            {
                Console.WriteLine($"Chat ID do grupo: {message.Chat.Id}");
                //await botClient.SendMessage(message.Chat.Id, $"O Chat ID deste grupo é: {message.Chat.Id}", cancellationToken: cancellationToken);
            }

            _commandService.HandleMessage(botClient, message, cancellationToken);
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Erro: {exception.Message}");
        return Task.CompletedTask;
    }
}

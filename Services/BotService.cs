using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class BotService
{
    private readonly ITelegramBotClient _botClient;
    private readonly BotCommandService _commandService;
    private readonly CancellationTokenSource _cts;

    public BotService(ITelegramBotClient botClient, BotCommandService commandService)
    {
        _botClient = botClient;
        _commandService = commandService;
        _cts = new CancellationTokenSource();
    }

    public void StartBot()
    {
        Console.WriteLine("🤖 Bot está iniciando...");
        RegisterCommands();

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions: new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // Receber todas as atualizações
            },
            cancellationToken: _cts.Token
        );
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

            await _commandService.HandleMessage(botClient, message, cancellationToken);
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Erro: {exception.Message}");
        return Task.CompletedTask;
    }

    private void RegisterCommands()
    {
        var commands = new[]
        {
            new Telegram.Bot.Types.BotCommand { Command = "consultartemporada", Description = "Verifica em qual temporada estamos" },
            new Telegram.Bot.Types.BotCommand { Command = "surtar", Description = "SURTOS" }
        };

        _botClient.SetMyCommands(commands);
        Console.WriteLine("📌 Comandos registrados no Telegram!");
    }
}

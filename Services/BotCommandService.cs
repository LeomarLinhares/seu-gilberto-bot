using SeuGilbertoBot.DTOs;
using SeuGilbertoBot.Models;
using SeuGilbertoBot.Services;
using System.Collections.Concurrent;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class BotCommandService
{
    private readonly DeepSeekService _deepSeekService;
    private readonly SeasonService _seasonService;
    private readonly UserService _userService;
    private readonly CartolaService _cartolaService;
    
    private static readonly ConcurrentDictionary<long, List<Message>> GroupMessages = new();

    public BotCommandService
    (
        DeepSeekService deepSeekService,
        SeasonService seasonService,
        UserService userService,
        CartolaService cartolaService
    )
    {
        _deepSeekService = deepSeekService;
        _seasonService = seasonService;
        _userService = userService;
        _cartolaService = cartolaService;
    }
    public async Task HandleMessage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
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
                case "/registrar":
                    var userRegisterResponse = await _userService.RegisterUser(new SeuGilbertoBot.Models.User {
                        FirstName = message.From.FirstName,
                        LastName = message.From.LastName,
                        Username = message.From.Username,
                        TelegramUserId = message.From.Id
                    });

                    botClient.SendMessage(chatId, userRegisterResponse, cancellationToken: cancellationToken);
                    break;

                case "/mercado":
                    CartolaStatusResponseDTO marketStatus = await _cartolaService.GetStatus();
                    string messageToMarketStatus = marketStatus.StatusMercado == 1
                        ? "✅ O mercado está <b>ABERTO</b>!"
                        : "❌ O mercado está <b>FECHADO</b>!";

                    await botClient.SendMessage(
                        chatId: chatId,
                        text: messageToMarketStatus,
                        parseMode: ParseMode.Html,
                        cancellationToken: cancellationToken
                    );

                    break;

                case "/consultartemporada":
                    var seasonInfo = await _seasonService.GetCurrentSeasonInfo();
                    Console.WriteLine(seasonInfo);
                    botClient.SendMessage(chatId, seasonInfo, cancellationToken: cancellationToken);
                    break;

                case "/surtar":
                    botClient.SendMessage(chatId, "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", cancellationToken: cancellationToken);
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

        if (messageText == null)
        {
            botClient.SendMessage(chatId, "Leos, por algum motivo a mensagem está chegando nula pra mim. Verifique isso por favor", cancellationToken: cancellationToken);
        }

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

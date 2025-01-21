using Telegram.Bot;

public class MessageSenderService
{
    private readonly ITelegramBotClient _botClient;

    public MessageSenderService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public void SendMessageToChat(long chatId, string message)
    {
        _botClient.SendMessage(chatId, message);
    }
}

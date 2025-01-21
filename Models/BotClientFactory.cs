using Microsoft.Extensions.Configuration;
using Telegram.Bot;

public static class BotClientFactory
{
    public static TelegramBotClient CreateBotClient(string token)
    {
        return new TelegramBotClient(token);
    }
}

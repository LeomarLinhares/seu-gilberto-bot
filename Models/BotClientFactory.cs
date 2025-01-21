using Microsoft.Extensions.Configuration;
using Telegram.Bot;

public static class BotClientFactory
{
    public static TelegramBotClient CreateBotClient()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var settings = configuration.GetSection("TelegramBot").Get<TelegramBotSettings>();
        return new TelegramBotClient(settings.Token);
    }
}

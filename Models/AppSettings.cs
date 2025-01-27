public class AppSettings
{
    public TelegramBotSettings TelegramBot { get; set; }
    public ConnectionStringsSettings ConnectionStrings { get; set; }
    public DeepSeek DeepSeek { get; set; }
}

public class TelegramBotSettings
{
    public string Token { get; set; }
    public string TargetGroup { get; set; }
}
public class DeepSeek
{
    public string ApiKey { get; set; }
    public string ApiUrl { get; set; }
}

public class ConnectionStringsSettings
{
    public string DefaultConnection { get; set; }
}

namespace Veloci.Logic.Bot;

public class TelegramMessage
{
    public TelegramMessage()
    {
    }

    public TelegramMessage(TelegramMessageType type, string text)
    {
        Type = type;
        Text = text;
    }

    public TelegramMessageType Type { get; set; }

    public string? FileUrl { get; set; }

    public string? Text { get; set; }
}

public enum TelegramMessageType
{
    NobodyFlying,
    FirstResult,
    OnlyOneFlew,
    VoteReminder
}

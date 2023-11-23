namespace Veloci.Logic.Bot;

public static class TelegramMessages
{
    private static readonly List<TelegramMessage> Messages = new ();
    private static readonly Random Random = new ();

    static TelegramMessages()
    {
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "👀 А де всі?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🧐 Є хто живий?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🫠 Трек сам себе не пролетить"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🙃 Може пора вже?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🙄 Чого чекаємо?"));
    }

    public static TelegramMessage? GetRandomByType(TelegramMessageType messageType)
    {
        if (!CalculateProbability())
            return null;

        var msgs = Messages.Where(m => m.Type == messageType).ToList();
        var r = Random.Next(msgs.Count);
        return msgs[r];
    }

    private static bool CalculateProbability()
    {
        const int probabilityPercentage = 25;
        var chance = Random.Next(1, 101);
        return chance <= probabilityPercentage;
    }
}

namespace Veloci.Logic.Bot;

public static class TelegramMessages
{
    private static readonly List<TelegramMessage> Messages = [];
    private static readonly Random Random = new ();

    static TelegramMessages()
    {
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "👀 А де всі?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🧐 Є хто живий?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🫠 Трек сам себе не пролетить"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🙃 Може пора вже?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🙄 Чого чекаємо?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🤓 Запускайте вже ваші симулятори"));
        Messages.Add(new TelegramMessage(TelegramMessageType.NobodyFlying, "🤔 Я новий трек для кого видав?"));

        Messages.Add(new TelegramMessage(TelegramMessageType.OnlyOneFlew, "👀 А де всі інші?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.OnlyOneFlew, "😐 Тільки один результат? Ви серйозно?"));
        Messages.Add(new TelegramMessage(TelegramMessageType.OnlyOneFlew, "🙄 Чого інші чекають?"));

        Messages.Add(new TelegramMessage(TelegramMessageType.VoteReminder, "👌 Не забудь оцінити трек"));
        Messages.Add(new TelegramMessage(TelegramMessageType.VoteReminder, "Оцінювати треки важливо 👆"));
        Messages.Add(new TelegramMessage(TelegramMessageType.VoteReminder, "Ну як тобі трек? 👆"));
        Messages.Add(new TelegramMessage(TelegramMessageType.VoteReminder, "Твоя думка важлива 👆"));
        Messages.Add(new TelegramMessage(TelegramMessageType.VoteReminder, "Оціни трек, якщо ще ні 👆"));
    }

    public static TelegramMessage GetRandomByType(TelegramMessageType messageType)
    {
        var msgs = Messages.Where(m => m.Type == messageType).ToList();
        var r = Random.Next(msgs.Count);
        return msgs[r];
    }

    public static TelegramMessage? GetRandomByTypeWithProbability(TelegramMessageType messageType)
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

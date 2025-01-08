namespace Veloci.Logic.Bot;

public static class ChatMessages
{
    private static readonly List<ChatMessage> Messages = [];
    private static readonly Random Random = new ();

    static ChatMessages()
    {
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "👀 А де всі?"));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "🧐 Є хто живий?"));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "🫠 Трек сам себе не пролетить"));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "🙃 Може пора вже?"));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "🙄 Чого чекаємо?"));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "🤓 Запускайте вже ваші симулятори"));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "😴 Zzzz..."));
        Messages.Add(new ChatMessage(ChatMessageType.NobodyFlying, "😕 Знову світла немає?"));

        Messages.Add(new ChatMessage(ChatMessageType.OnlyOneFlew, "👀 А де всі інші?"));
        Messages.Add(new ChatMessage(ChatMessageType.OnlyOneFlew, "😐 Тільки один результат? Ви серйозно?"));
        Messages.Add(new ChatMessage(ChatMessageType.OnlyOneFlew, "🙄 Чого інші чекають?"));

        Messages.Add(new ChatMessage(ChatMessageType.VoteReminder, "👌 Не забудь оцінити трек"));
        Messages.Add(new ChatMessage(ChatMessageType.VoteReminder, "Оцінювати треки важливо 👆"));
        Messages.Add(new ChatMessage(ChatMessageType.VoteReminder, "Ну як тобі трек? 👆"));
        Messages.Add(new ChatMessage(ChatMessageType.VoteReminder, "Твоя думка важлива 👆"));
        Messages.Add(new ChatMessage(ChatMessageType.VoteReminder, "Оціни трек, якщо ще ні 👆"));
    }

    public static ChatMessage GetRandomByType(ChatMessageType messageType)
    {
        var msgs = Messages.Where(m => m.Type == messageType).ToList();
        var r = Random.Next(msgs.Count);
        return msgs[r];
    }

    public static ChatMessage? GetRandomByTypeWithProbability(ChatMessageType messageType)
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

namespace Veloci.Logic.Bot;

public class BotPoll
{
    public string Question { get; set; }

    public List<BotPollOption> Options { get; set; }
}

public class BotPollOption
{
    public BotPollOption()
    {

    }

    public BotPollOption(int points, string text)
    {
        Points = points;
        Text = text;
    }

    public int Points { get; set; }

    public string Text { get; set; }
}

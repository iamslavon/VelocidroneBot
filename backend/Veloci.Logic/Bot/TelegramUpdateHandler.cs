using Hangfire;
using Telegram.Bot;
using Telegram.Bot.Types;
using Veloci.Data.Domain;
using Veloci.Data.Repositories;
using Veloci.Logic.Services;

namespace Veloci.Logic.Bot;

public interface ITelegramUpdateHandler
{
    Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly CompetitionConductor _competitionConductor;
    private readonly IRepository<Pilot> _pilots;

    public TelegramUpdateHandler(
        CompetitionConductor competitionConductor,
        IRepository<Pilot> pilots)
    {
        _competitionConductor = competitionConductor;
        _pilots = pilots;
    }

    public async Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message ?? update.ChannelPost;

        if (message is null)
            return;

        var text = message.Text;

        if (string.IsNullOrEmpty(text))
            return;

        if (MessageParser.IsCompetitionRestart(text))
        {
            if (!TelegramBot.IsMainChannelId(message.Chat.Id.ToString()))
                return;

            await TelegramBot.SendMessageAsync("Добре 🫡");
            BackgroundJob.Schedule(() => _competitionConductor.StartNewAsync(), new TimeSpan(0, 0, 5));
        }

        if (MessageParser.IsCurrentDayStreakCommand(text))
            await ProcessCurrentDayStreakCommandAsync(message);
    }

    private async Task ProcessCurrentDayStreakCommandAsync(Message message)
    {
        var text = message.Text;
        var pilotName = MessageParser.GetCurrentDayStreakCommandParameter(text);

        var pilot = await _pilots.FindAsync(pilotName);

        if (pilot is null)
        {
            await TelegramBot.ReplyMessageAsync("не знаю такого пілота 😕", message.MessageId, message.Chat.Id.ToString());
        }
        else
        {
            await TelegramBot.ReplyMessageAsync($"{pilot.DayStreak}", message.MessageId, message.Chat.Id.ToString());
        }
    }
}

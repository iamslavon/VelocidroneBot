using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;
using Veloci.Logic.Services;

namespace Veloci.Logic.Bot;

public interface ITelegramUpdateHandler
{
    Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}

public class TelegramUpdateHandler : ITelegramUpdateHandler
{
    private readonly CompetitionConductor _competitionConductor;

    public TelegramUpdateHandler(CompetitionConductor competitionConductor)
    {
        _competitionConductor = competitionConductor;
    }

    public async Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message ?? update.ChannelPost;

        if (message is null)
            return;

        var text = message.Text;

        if (string.IsNullOrEmpty(text))
            return;

        if (MessageParser.IsStartCompetition(text))
        {
            try
            {
                await _competitionConductor.StartNewAsync(text, message.Chat.Id);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to start competition");
            }
        }

        if (MessageParser.IsStopCompetition(text))
        {
            try
            {
                await _competitionConductor.StopAsync(text, message.Chat.Id, message.MessageId);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to stop competition");
            }
        }
    }
}

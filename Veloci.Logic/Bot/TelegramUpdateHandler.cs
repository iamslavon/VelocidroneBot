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
    private readonly CompetitionService _competitionService;

    public TelegramUpdateHandler(CompetitionService competitionService)
    {
        _competitionService = competitionService;
    }

    public async Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is null)
            return;

        var text = update.Message.Text;

        if (string.IsNullOrEmpty(text))
            return;

        if (MessageParser.IsStartCompetition(text))
        {
            try
            {
                await _competitionService.StartNewAsync(text, update.Message.Chat.Id);
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
                await _competitionService.StopAsync(text, update.Message.Chat.Id);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to stop competition");
            }
        }
    }
}

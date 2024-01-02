using Hangfire;
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
    private readonly ImageService _imageService;

    public TelegramUpdateHandler(CompetitionConductor competitionConductor, ImageService imageService)
    {
        _competitionConductor = competitionConductor;
        _imageService = imageService;
    }

    public async Task OnUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message ?? update.ChannelPost;

        if (message is null)
            return;

        var text = message.Text;

        if (string.IsNullOrEmpty(text))
            return;

        if (text == "image test")
        {
            var imageStream = await _imageService.CreateWinnerImageAsync("January 2024", "Pilot Name");
            await TelegramBot.SendPhotoAsync(message.Chat.Id, imageStream);
        }

        if (MessageParser.IsCompetitionRestart(text))
        {
            await TelegramBot.SendMessageAsync("Добре 🫡");
            BackgroundJob.Schedule(() => _competitionConductor.StartNewAsync(), new TimeSpan(0, 0, 5));
        }
    }
}

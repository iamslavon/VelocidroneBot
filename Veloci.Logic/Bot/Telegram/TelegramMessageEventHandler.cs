using Hangfire;
using MediatR;
using Veloci.Logic.Notifications;
using Veloci.Logic.Services;

namespace Veloci.Logic.Bot.Telegram;

public class TelegramMessageEventHandler :
    INotificationHandler<IntermediateCompetitionResult>,
    INotificationHandler<CurrentResultUpdateMessage>,
    INotificationHandler<CompetitionStopped>,
    INotificationHandler<CompetitionStarted>,
    INotificationHandler<TempSeasonResults>,
    INotificationHandler<SeasonFinished>,
    INotificationHandler<BadTrack>,
    INotificationHandler<CheerUp>
{
    private readonly MessageComposer _messageComposer;

    public TelegramMessageEventHandler(MessageComposer messageComposer)
    {
        _messageComposer = messageComposer;
    }

    public async Task Handle(IntermediateCompetitionResult notification, CancellationToken cancellationToken)
    {
        var message = _messageComposer.TempLeaderboard(notification.Leaderboard);
        await TelegramBot.SendMessageAsync(message);
    }

    public async Task Handle(CurrentResultUpdateMessage notification, CancellationToken cancellationToken)
    {
        var message = _messageComposer.TimeUpdate(notification.Delatas);
        await TelegramBot.SendMessageAsync(message);
    }

    public async Task Handle(CompetitionStopped notification, CancellationToken cancellationToken)
    {
        var competition = notification.Competition;

        if (competition.CompetitionResults.Count == 0)
            return;

        var resultsMessage = _messageComposer.Leaderboard(competition.CompetitionResults, competition.Track.FullName);
        await TelegramBot.SendMessageAsync(resultsMessage);
    }

    public async Task Handle(CompetitionStarted notification, CancellationToken cancellationToken)
    {
        var startCompetitionMessage = _messageComposer.StartCompetition(notification.Track);
        await TelegramBot.SendMessageAsync(startCompetitionMessage);
    }

    public async Task Handle(TempSeasonResults notification, CancellationToken cancellationToken)
    {
        var message = _messageComposer.TempSeasonResults(notification.Results);
        await TelegramBot.SendMessageAsync(message);
    }

    public async Task Handle(SeasonFinished notification, CancellationToken cancellationToken)
    {
        var message = _messageComposer.SeasonResults(notification.Results);
        await TelegramBot.SendMessageAsync(message);

        await TelegramBot.SendPhotoAsync(new MemoryStream(notification.Image));

        var medalCountMessage = _messageComposer.MedalCount(notification.Results);
        BackgroundJob.Schedule(() => TelegramBot.SendMessageAsync(medalCountMessage), TimeSpan.FromSeconds(6));
    }

    public async Task Handle(BadTrack notification, CancellationToken cancellationToken)
    {
        var message = _messageComposer.BadTrackRating();
        await TelegramBot.SendMessageAsync(message);
    }

    public async Task Handle(CheerUp notification, CancellationToken cancellationToken)
    {
        var cheerUpMessage = notification.Message;

        if (cheerUpMessage.FileUrl is null && cheerUpMessage.Text is not null)
        {
            await TelegramBot.SendMessageAsync(cheerUpMessage.Text);
            return;
        }

        if (cheerUpMessage.FileUrl is not null)
        {
            await TelegramBot.SendPhotoAsync(cheerUpMessage.FileUrl, cheerUpMessage.Text);
        }
    }
}

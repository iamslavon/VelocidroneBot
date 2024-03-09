using MediatR;
using Veloci.Data.Domain;
using Veloci.Logic.Bot;

namespace Veloci.Logic.Notifications;

public record IntermediateCompetitionResult(List<CompetitionResults> Leaderboard, Competition Competition) : INotification;

public record CompetitionStopped (Competition Competition) : INotification;

public record CompetitionStarted(Competition Competition, Track Track) : INotification;

public record TempSeasonResults(List<SeasonResult> Results) : INotification;

public record SeasonFinished(List<SeasonResult> Results, string SeasonName, string WinnerName, byte[] Image) : INotification;

public record BadTrack(Competition Competition, Track Track) : INotification;

public record CheerUp(ChatMessage Message) : INotification;

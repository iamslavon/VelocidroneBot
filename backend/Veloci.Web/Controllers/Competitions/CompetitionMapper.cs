using Riok.Mapperly.Abstractions;
using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Competitions;

[Mapper]
public static partial class CompetitionMapper
{
    public static CompetitionModel MapToModel(this Competition competition)
    {
        return new CompetitionModel
        {
            MapName = competition.Track.Map.Name
        };
    }
    public static partial IQueryable<CompetitionModel> ProjectToModel(this IQueryable<Competition> competitions);

    public static partial List<TrackTimeModel> MapToModel(this IEnumerable<TrackTime> results);

    public static partial List<SeasonResultModel> MapToModel(this IEnumerable<SeasonResult> results);

    public static partial List<TrackTimeModel> MapToModel(this IEnumerable<CompetitionResults> results);

    [MapProperty(nameof(CompetitionResults.TrackTime), nameof(TrackTimeModel.Time))]
    public static partial TrackTimeModel MapToModel(this CompetitionResults results);
}

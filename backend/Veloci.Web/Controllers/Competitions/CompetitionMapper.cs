using Riok.Mapperly.Abstractions;
using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Competitions;

[Mapper]
public static partial class CompetitionMapper
{
    public static partial CompetitionModel MapToModel(this Competition competition);
    public static partial IQueryable<CompetitionModel> ProjectToModel(this IQueryable<Competition> competitions);

    public static partial List<TrackTimeModel> MapToModel(this IEnumerable<TrackTime> results);
}

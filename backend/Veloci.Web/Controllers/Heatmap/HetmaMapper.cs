using Riok.Mapperly.Abstractions;
using Veloci.Data.Domain;

namespace Veloci.Web.Controllers.Heatmap;


[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public static partial class HeatmapMapper
{
    [MapProperty(nameof(@CompetitionResults.Competition.StartedOn), nameof(HeatmapEntry.Date))]
    public static partial HeatmapEntry ProjectToModel(this CompetitionResults results);

    public static partial IQueryable<HeatmapEntry> ProjectToModel(this IQueryable<CompetitionResults> results);
}

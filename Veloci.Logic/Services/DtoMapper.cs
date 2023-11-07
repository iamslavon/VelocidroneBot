using Riok.Mapperly.Abstractions;
using Veloci.Logic.Domain;
using Veloci.Logic.Dto;

namespace Veloci.Logic.Services;

[Mapper]
public partial class DtoMapper
{
    [MapProperty(nameof(TrackTimeDto.Playername), nameof(TrackTime.PlayerName))]
    public partial TrackTime MapTrackTime(TrackTimeDto timesDtos);
}
using Veloci.Logic.Services.Tracks.Models;

namespace Veloci.Logic.Services.Tracks;

public interface ITrackFetcher
{
    Task<List<ParsedMapModel>> FetchMapsAsync();
}

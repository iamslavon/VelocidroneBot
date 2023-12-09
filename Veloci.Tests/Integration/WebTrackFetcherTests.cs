using Veloci.Logic.Services.Tracks;

namespace Veloci.Tests.Integration;

public class WebTrackFetcherTests
{
    [Fact]
    [Trait ("Category", "Integration")]
    public async Task can_fetch_track_via_web()
    {
        var fetcher = new WebTrackFetcher();
        var maps = await fetcher.FetchMapsAsync();
    }

    [Fact]
    [Trait ("Category", "Integration")]
    public async Task can_fetch_track_via_api()
    {
        var fetcher = new ApiTrackFetcher();
        var maps = await fetcher.FetchMapsAsync();
    }
}

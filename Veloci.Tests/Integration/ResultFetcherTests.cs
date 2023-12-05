using System.Text.Json;
using Veloci.Logic.Services;

namespace Veloci.Tests.Integration;

public class ResultFetcherTests
{
    [Fact]
    [Trait ("Category", "Integration")]
    public async Task can_get_data()
    {
        var f = new ResultsFetcher();

        var data = await f.FetchAsync(814);
        var json = JsonSerializer.Serialize(data);
    }
}

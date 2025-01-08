using System.Text.Json;
using Veloci.Logic.Dto;
using Veloci.Logic.Services.Tracks;

namespace Veloci.Logic.Services;

public class ResultsFetcher
{
    private static readonly HttpClient Client = new HttpClient();

    public async Task<IList<TrackTimeDto>?> FetchAsync(int trackId)
    {

        var requestData = $"track_id={trackId}&sim_version=1.16&offset=0&count=1000&protected_track_value=1&race_mode=6";
        var encrypted = Encryption.Encrypt(requestData);

        var parameters = new Dictionary<string, string>
        {
            { "post_data", encrypted }
        };

        var response = await Client.PostAsync($"{VelocidroneApiConstants.BaseUrl}/api/leaderboard/getLeaderBoard", new FormUrlEncodedContent (parameters));
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var decrypted = Encryption.Decrypt(responseBody);
        var results = JsonSerializer.Deserialize<TrackResultsDto>(decrypted);

        return results?.Tracktimes;
    }
}

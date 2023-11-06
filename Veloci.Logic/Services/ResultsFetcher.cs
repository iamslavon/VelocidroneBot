using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Veloci.Logic.Dto;

namespace Veloci.Logic.Services;

public class ResultsFetcher
{
    private static readonly HttpClient Client = new HttpClient();
    
    public async Task<IList<TrackTimeDto>?> FetchAsync(int trackId)
    {
        const string key = "BatCaveGGevaCtaB";
        var requestData = $"track_id={trackId}&sim_version=1.16&offset=0&count=1000&protected_track_value=1&race_mode=6";
        var encrypted = Encrypt(requestData, key);
        
        var parameters = new Dictionary<string, string>
        {
            { "post_data", encrypted }
        };
        
        var response = await Client.PostAsync("http://www.velocidrone.com/api/leaderboard/getLeaderBoard", new FormUrlEncodedContent (parameters));
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var decrypted = Decrypt(responseBody, key);
        var results = JsonConvert.DeserializeObject<TrackResultsDto>(decrypted);

        return results?.Tracktimes;
    }
    
    private string Decrypt(string content, string keyString)
    {
        var buffer = Convert.FromBase64String(content);
        var key = Encoding.UTF8.GetBytes(keyString);
        var rijndaelManaged = Aes.Create();
        rijndaelManaged.Key = key;
        rijndaelManaged.Mode = CipherMode.ECB;
        rijndaelManaged.BlockSize = 128;
        rijndaelManaged.Padding = PaddingMode.PKCS7;
        
        var cryptoTransform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
        using var stream = new MemoryStream(buffer);
        using var stream2 = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(stream2);
        
        return streamReader.ReadToEnd();
    }
        
    private string Encrypt(string content, string keyString)
    {
        var obj = Aes.Create();
        obj.Key = Encoding.UTF8.GetBytes(keyString);
        obj.Mode = CipherMode.ECB;
        obj.BlockSize = 128;
        obj.Padding = PaddingMode.PKCS7;
        
        var cryptoTransform = obj.CreateEncryptor(obj.Key, null);
        using var memoryStream = new MemoryStream();
        using var stream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
        using (var streamWriter = new StreamWriter(stream))
        {
            streamWriter.Write(content);
            streamWriter.Flush();
        }
        
        return Convert.ToBase64String(memoryStream.ToArray());
    }
}
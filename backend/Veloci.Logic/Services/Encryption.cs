using System.Security.Cryptography;
using System.Text;

namespace Veloci.Logic.Services;

public static class Encryption
{
    private const string key = "BatCaveGGevaCtaB";

    public static string Encrypt(string content, string keyString = key)
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

    public static string Decrypt(string content, string keyString = key)
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
}

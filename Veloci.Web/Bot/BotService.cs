using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Veloci.Web.Bot;

public class BotService
{
    private static TelegramSettings _telegramSettings;
    private static TelegramBotClient _client;

    public static void Init(IConfiguration configuration)
    {
        _telegramSettings = configuration.GetSection("Telegram").Get<TelegramSettings>();
        _client = new TelegramBotClient(_telegramSettings.BotToken);
        
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = {} // receive all update types
        };
        
        _client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
            );
    }
    
    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is null)
            return;

        var message = update.Message;

        if (string.IsNullOrEmpty(message.Text))
            return;
        
        if (message.Text.Contains("Трек дня"))
        {
            var trackName = GetTrackName(message.Text);
            var trackId = GetTrackId(message.Text);
            await _client.SendTextMessageAsync(message.Chat, $"Id: {trackId} Name: {trackName}", cancellationToken: cancellationToken);
        }
    }

    private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ApiRequestException apiRequestException)
        {
            
        }
    }

    private static string GetTrackName(string text)
    {
        var lines = text.Split('\n');

        if (lines.Length > 1)
            return lines[1];

        throw new Exception("Could not get track name from the message");
    }

    private static int GetTrackId(string text)
    {
        const string pattern = @"https://www.velocidrone.com/leaderboard/16/(\d+)/All";
        var match = Regex.Match(text, pattern);

        if (match.Success)
        {
            var idString = match.Groups[1].Value;

            if (int.TryParse(idString, out var id))
            {
                return id;
            }
        }

        throw new Exception("Track id not found");
    }
    
    public async Task SendMessageAsync(string message)
    {
        try
        {
            var result = await _client.SendTextMessageAsync(
                chatId: _telegramSettings.ChannelId,
                text: Isolate(message),
                parseMode: ParseMode.MarkdownV2);
        }
        catch (Exception ex)
        {
            //Log.Error(ex, "Telegram. Failed to send a message '{Message}'", message);
        }
    }
    
    private string Isolate(string message) => message.Replace(".", "\\.").Replace("!", "\\!").Replace("-", "\\-");
}


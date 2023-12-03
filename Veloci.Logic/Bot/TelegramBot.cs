using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Veloci.Logic.Services;

namespace Veloci.Logic.Bot;

public class TelegramBot
{
    private readonly IServiceProvider _sp;
    private static string _botToken;
    private static string _channelId;
    private static TelegramBotClient _client;
    private static CompetitionService _competitionService;
    private CancellationTokenSource _cts;

    public TelegramBot(IConfiguration configuration, IServiceProvider sp)
    {
        _sp = sp;
        _botToken = configuration.GetSection("Telegram:BotToken").Value;
        _channelId = configuration.GetSection("Telegram:ChannelId").Value;
    }

    public void Init()
    {
        if (string.IsNullOrEmpty(_botToken)) return;

        _client = new TelegramBotClient(_botToken);
        _cts = new CancellationTokenSource();
        var cancellationToken = _cts.Token;

        StartReceiving(cancellationToken);
    }

    private void StartReceiving(CancellationToken ct)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { } // receive all update types
        };

        _client.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            ct
        );
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        using var scope = _sp.CreateScope();
        var updater = scope.ServiceProvider.GetRequiredService<ITelegramUpdateHandler>();
        await updater.OnUpdateAsync(botClient, update, cancellationToken);
    }

    private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Log.Error(exception, "Error in telegram bot");
    }

    public static async Task SendMessageAsync(string message)
    {
        try
        {
            var result = await _client.SendTextMessageAsync(
                chatId: _channelId,
                text: Isolate(message),
                parseMode: ParseMode.MarkdownV2);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Telegram. Failed to send a message '{Message}'", message);
        }
    }

    public static async Task EditMessageAsync(string message, long chatId, int messageId)
    {
        try
        {
            var result = await _client.EditMessageTextAsync(
                chatId: chatId,
                messageId: messageId,
                parseMode: ParseMode.MarkdownV2,
                text: Isolate(message));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Telegram. Failed to edit a message '{Message}'", message);
        }
    }

    public static async Task SendPhotoAsync(long chatId, string fileUrl, string? message = null)
    {
        if (message is not null)
            message = Isolate(message);

        try
        {
            var result = await _client.SendPhotoAsync(
                chatId: chatId,
                caption: message,
                photo: new InputFileUrl(fileUrl)
            );
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Telegram. Failed to send a photo");
        }
    }

    public static async Task SendPhotoAsync(long chatId, Stream file, string? message = null)
    {
        file.Position = 0; // Weird fix. It throws an exception without

        if (message is not null)
            message = Isolate(message);

        try
        {
            var result = await _client.SendPhotoAsync(
                chatId: chatId,
                photo: new InputFileStream(file),
                caption: message
            );
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Telegram. Failed to send a photo");
        }
        finally
        {
            await file.DisposeAsync();
        }
    }

    public static async Task<int> SendPollAsync(string question, IEnumerable<string> options)
    {
        var message = await _client.SendPollAsync(
            chatId: _channelId,
            question: question,
            options: options
            );

        return message.MessageId;
    }

    private static string Isolate(string message) => message
        .Replace(".", "\\.")
        .Replace("!", "\\!")
        .Replace("-", "\\-")
        .Replace("_", "\\_")
        .Replace(")", "\\)")
        .Replace("(", "\\(")
        .Replace("#", "\\#");

    public void Stop()
    {
        _cts.Cancel();
    }
}

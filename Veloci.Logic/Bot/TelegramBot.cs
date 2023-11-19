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
    private static TelegramBotClient _client;
    private static CompetitionService _competitionService;
    private CancellationTokenSource _cts;

    public TelegramBot(IConfiguration configuration, IServiceProvider sp)
    {
        _sp = sp;
        _botToken = configuration.GetSection("Telegram:BotToken").Value;
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

    public static async Task SendMessageAsync(string message, long chatId)
    {
        try
        {
            var result = await _client.SendTextMessageAsync(
                chatId: chatId,
                text: Isolate(message),
                parseMode: ParseMode.MarkdownV2);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Telegram. Failed to send a message '{Message}'", message);
        }
    }

    private static string Isolate(string message) => message
        .Replace(".", "\\.")
        .Replace("!", "\\!")
        .Replace("-", "\\-")
        .Replace(")", "\\)")
        .Replace("(", "\\(")
        .Replace("#", "\\#");

    public void Stop()
    {
        _cts.Cancel();
    }
}

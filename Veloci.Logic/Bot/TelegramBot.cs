using Microsoft.Extensions.Configuration;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Veloci.Logic.Services;

namespace Veloci.Logic.Bot;

public class TelegramBot
{
    private static string _botToken;
    private static TelegramBotClient _client;
    private static CompetitionService _competitionService;
    private CancellationTokenSource _cts;

    public TelegramBot(CompetitionService competitionService, IConfiguration configuration)
    {
        _competitionService = competitionService;
        _botToken = configuration.GetSection("Telegram:BotToken").Value;
    }

    public void Init()
    {
        _client = new TelegramBotClient(_botToken);
        _cts = new CancellationTokenSource();
        var cancellationToken = _cts.Token;

        StartReceiving(cancellationToken);
    }

    private static void StartReceiving(CancellationToken ct)
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

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        if (update.Message is null)
            return;

        var text = update.Message.Text;

        if (string.IsNullOrEmpty(text))
            return;

        if (MessageParser.IsStartCompetition(text))
        {
            try
            {
                await _competitionService.StartNewAsync(text, update.Message.Chat.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        if (MessageParser.IsStopCompetition(text))
        {
            try
            {
                await _competitionService.StopAsync(text, update.Message.Chat.Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
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
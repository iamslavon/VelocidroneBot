using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Veloci.Logic.Services;

namespace Veloci.Logic.Bot;

public static class TelegramBot
{
    private static string _botToken;
    private static TelegramBotClient _client;
    private static CompetitionService _competitionService;

    public static void Init(IConfiguration configuration, IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _competitionService = serviceProvider.GetService<CompetitionService>();
        _botToken = configuration.GetSection("Telegram:BotToken").Value;
        _client = new TelegramBotClient(_botToken);

        StartReceiving();
    }

    private static void StartReceiving()
    {
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

    private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(exception);
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
            //Log.Error(ex, "Telegram. Failed to send a message '{Message}'", message);
        }
    }
    
    private static string Isolate(string message) => message
        .Replace(".", "\\.")
        .Replace("!", "\\!")
        .Replace("-", "\\-")
        .Replace(")", "\\)")
        .Replace("(", "\\(")
        .Replace("#", "\\#");
}


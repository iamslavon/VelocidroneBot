using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Veloci.Logic.Services;

namespace Veloci.Web.Bot;

public class TelegramBot
{
    private static TelegramSettings _telegramSettings;
    private static TelegramBotClient _client;
    private static IServiceProvider _serviceProvider;

    public static void Init(IConfiguration configuration, IServiceCollection serviceCollection)
    {
        _serviceProvider = serviceCollection.BuildServiceProvider();
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
        
        if (MessageParser.IsStartCompetition(message.Text))
        {
            // start
            var trackName = MessageParser.GetTrackName(message.Text);
            var trackId = MessageParser.GetTrackId(message.Text);
            //await _client.SendTextMessageAsync(message.Chat, $"Id: {trackId} Name: {trackName}", cancellationToken: cancellationToken);
        }

        if (MessageParser.IsStopCompetition(message.Text))
        {
            // stop
        }
    }

    private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ApiRequestException apiRequestException)
        {
            
        }
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


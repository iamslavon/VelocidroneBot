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
    private static CompetitionService _competitionService;

    public static void Init(IConfiguration configuration, IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _competitionService = serviceProvider.GetService<CompetitionService>();
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

        var text = update.Message.Text;

        if (string.IsNullOrEmpty(text))
            return;
        
        if (MessageParser.IsStartCompetition(text))
            await _competitionService.StartNewAsync(text, update.Message.Chat.Id);

        if (MessageParser.IsStopCompetition(text))
            await _competitionService.StopAsync(text, update.Message.Chat.Id);
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


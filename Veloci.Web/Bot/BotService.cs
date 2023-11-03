using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Veloci.Web.Bot;

public class BotService
{
    private readonly TelegramSettings _telegramSettings;
    private readonly TelegramBotClient _client;

    public BotService(IConfiguration configuration)
    {
        _telegramSettings = configuration.GetSection("Telegram").Get<TelegramSettings>();
        _client = new TelegramBotClient(_telegramSettings.BotToken);
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


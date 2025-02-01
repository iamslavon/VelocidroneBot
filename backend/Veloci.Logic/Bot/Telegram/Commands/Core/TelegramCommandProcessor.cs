using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types;

namespace Veloci.Logic.Bot.Telegram.Commands.Core;

public class TelegramCommandProcessor
{
    private readonly IServiceProvider _serviceProvider;

    public TelegramCommandProcessor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ProcessAsync(Message message)
    {
        var text = message.Text;
        var parsed = ParseMessage(text);

        if (parsed is null)
            return;

        var command = GetCommand(parsed.Command);

        if (command is null)
            return;

        var result = await command.ExecuteAsync(parsed.Parameters);
        var messageId = await TelegramBot.ReplyMessageAsync(result, message.MessageId, message.Chat.Id.ToString());

        if (messageId.HasValue && command.RemoveMessageAfterDelay)
        {
            BackgroundJob.Schedule(() => TelegramBot.RemoveMessageAsync(messageId.Value, message.Chat.Id.ToString()), TimeSpan.FromSeconds(60));
        }
    }

    private ParsedMessage? ParseMessage(string? text)
    {
        if (string.IsNullOrEmpty(text))
            return null;

        var split = text.Split(' ');
        var command = split.First();

        if (!command.StartsWith('/'))
            return null;

        return new ParsedMessage
        {
            Command = command.ToLower(),
            Parameters = split.Skip(1).ToArray()
        };
    }

    private ITelegramCommand? GetCommand(string command)
    {
        return _serviceProvider
            .GetServices<ITelegramCommand>()
            .FirstOrDefault(c => c.Keywords.Contains(command));
    }
}

public class ParsedMessage
{
    public required string Command { get; set; }
    public string[]? Parameters { get; set; }
}


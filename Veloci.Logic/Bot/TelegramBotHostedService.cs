using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Veloci.Logic.Bot;

public class TelegramBotHostedService : IHostedService, IDisposable
{
    private TelegramBot _telegramBot;
    private IServiceScope _scope;

    public TelegramBotHostedService(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
        _telegramBot = _scope.ServiceProvider.GetRequiredService<TelegramBot>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Log.Information("Starting telegram bot");
        _telegramBot.Init();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Stopping telegram bot");
        _telegramBot.Stop();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}

public class DiscordBotHostedService : IHostedService, IDisposable
{
    private DiscordBot _bot;
    private IServiceScope _scope;

    public DiscordBotHostedService(IServiceProvider serviceProvider)
    {
        _scope = serviceProvider.CreateScope();
        _bot = _scope.ServiceProvider.GetRequiredService<IDiscordBot>() as DiscordBot;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _bot.StartAsync();
        Log.Information("Starting telegram bot");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Stopping telegram bot");
        await _bot.Stop();
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}

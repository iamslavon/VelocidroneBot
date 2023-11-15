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
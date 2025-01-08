using Microsoft.Extensions.DependencyInjection;

namespace Veloci.Logic.Bot;

public static class TelegramBotServiceHelper
{
    public static void UseTelegramBotService(this IServiceCollection services)
    {
        services.AddHostedService<TelegramBotHostedService>();
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Veloci.Logic.Bot;

public static class TelegramBotServiceHelper
{
    public static void UserTelegramBotService(this IServiceCollection services)
    {
        services.AddHostedService<TelegramBotHostedService>();
    }
}
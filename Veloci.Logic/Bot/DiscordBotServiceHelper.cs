using Microsoft.Extensions.DependencyInjection;

namespace Veloci.Logic.Bot;

public static class DiscordBotServiceHelper
{
    public static void UseDiscordBotService(this IServiceCollection services)
    {
        services.AddSingleton<IDiscordBot, DiscordBot>();
        services.AddHostedService<DiscordBotHostedService>();
    }
}

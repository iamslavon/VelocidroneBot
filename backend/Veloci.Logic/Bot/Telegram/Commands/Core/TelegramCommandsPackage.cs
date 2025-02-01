using Microsoft.Extensions.DependencyInjection;

namespace Veloci.Logic.Bot.Telegram.Commands.Core;

public static class TelegramCommandsPackage
{
    public static IServiceCollection RegisterTelegramCommands(this IServiceCollection services)
    {
        services
            .AddTransient<TelegramCommandProcessor>()
            .AddTransient<ITelegramCommand, HelpCommand>()
            .AddTransient<ITelegramCommand, CurrentDayStreakCommand>()
            .AddTransient<ITelegramCommand, MaxDayStreakCommand>()
            .AddTransient<ITelegramCommand, TotalFlightDaysCommand>()
            ;

        return services;
    }
}

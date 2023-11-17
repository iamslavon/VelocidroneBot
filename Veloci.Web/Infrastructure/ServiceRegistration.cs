using Veloci.Data.Repositories;
using Veloci.Logic.Bot;
using Veloci.Logic.Services;

namespace Veloci.Web.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<ResultsFetcher>();
        services.AddTransient<CompetitionService>();
        services.AddTransient<RaceResultsConverter>();
        services.AddTransient<MessageComposer>();
        services.AddTransient<RaceResultDeltaAnalyzer>();
        services.AddTransient<TelegramBot>();
        services.AddTransient<ITelegramUpdateHandler, TelegramUpdateHandler>();

        return services;
    }
}
